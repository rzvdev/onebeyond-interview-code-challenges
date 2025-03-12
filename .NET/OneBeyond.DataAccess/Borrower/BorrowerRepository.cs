using Microsoft.EntityFrameworkCore;
using OneBeyond.DomainLogic.Utility;
using OneBeyond.Model.Core;
using OneBeyond.Model.DTOs;
using OneBeyond.Model.Entities;

namespace OneBeyond.DataAccess;

public class BorrowerRepository : IBorrowerRepository
{
    public BorrowerRepository()
    {
    }
    public List<Borrower> GetBorrowers()
    {
        using (var context = new LibraryContext())
        {
            var list = context.Borrowers
                .ToList();
            return list;
        }
    }

    public Guid AddBorrower(Borrower borrower)
    {
        using (var context = new LibraryContext())
        {
            context.Borrowers.Add(borrower);
            context.SaveChanges();
            return borrower.Id;
        }
    }

    public ApiResponse<List<BorrowerLoanDto>> GetBorrowersLoans() {
        using var context = new LibraryContext();

        var bookStocks = context.Catalogue.ToList();

        var borrowers = context.Catalogue.Where(bs => bs.OnLoanTo != null)
                                         .Include(bs => bs.OnLoanTo)
                                         .Include(bs => bs.Book)
                                         .AsEnumerable()
                                         .Select(bs => new BorrowerLoanDto(
                                            BorrowerName: bs.OnLoanTo!.Name,
                                            BorrowerEmail: bs.OnLoanTo!.EmailAddress,
                                            BookTitle: bs.Book.Name,
                                            LoadEndDate: bs.LoanEndDate
                                         )).ToList();

        return borrowers.Count != 0
            ? ApiResponse<List<BorrowerLoanDto>>.SuccessResponse(borrowers, "Borrowers with active loans retrieved successfully")
            : ApiResponse<List<BorrowerLoanDto>>.ErrorResponse("No borrowers found with active loans.");
    }

    public ApiResponse<Guid> MarkBookAsReturned(Guid bookStockId) {
        using var context = new LibraryContext();

        var bookStock = context.Catalogue.Include(bs => bs.OnLoanTo)
                                         .FirstOrDefault(bs => bs.Id == bookStockId && bs.OnLoanTo != null);

        if (bookStock is null)
            return ApiResponse<Guid>.ErrorResponse("Book not found");
        if (bookStock.OnLoanTo is null)
            return ApiResponse<Guid>.ErrorResponse("Book is not currently on loan");

        // check for fine
        var returnDate = DateTime.UtcNow;
        decimal fineAmount = 0;

        if (bookStock.LoanEndDate.HasValue && returnDate > bookStock.LoanEndDate.Value) {
            fineAmount = FineCalculator.CalculateFine(bookStock.LoanEndDate.Value, returnDate);

            context.Fines.Add(new() {
                Borrower = bookStock.OnLoanTo,
                BookStock = bookStock,
                Amount = fineAmount
            });
        }

        // mark the book as returned
        bookStock.OnLoanTo = null;
        bookStock.LoanEndDate = null;
        context.SaveChanges();

        return fineAmount > 0 
            ? ApiResponse<Guid>.SuccessResponse(bookStockId, $"Book returned late. Fine imposed: ${fineAmount}")
            : ApiResponse<Guid>.SuccessResponse(bookStockId, "Book successfully returned");
    }

    public ApiResponse<Guid> ReserveBook(Guid borrowerId, Guid bookId) {
        using var context = new LibraryContext();

        // check if the book have already a reservation
        var bookStock = context.Catalogue.Include(bs => bs.Book)
                                         .Where(bs => bs.Book.Id == bookId && bs.OnLoanTo != null)
                                         .FirstOrDefault();

        if (bookStock == null)
            return ApiResponse<Guid>.ErrorResponse("This book is not currently on loan and does not require a reservation");

        // check if book was already reserved by that borrower
        var reservationExists = context.Reservations.Include(r => r.Borrower)
                                                    .Include(r => r.Book)
                                                    .Any(r => r.Borrower.Id == borrowerId && r.Book.Id == bookId && r.IsActive);

        if (reservationExists)
            return ApiResponse<Guid>.ErrorResponse("You have already reserved that book");

        // check the queue for that book reservation
        var queuePosition = context.Reservations.Include(r => r.Book)
                                                 .Where(r => r.Book.Id == bookId && r.IsActive)
                                                 .Count() + 1;

        var expectedAvailabilityDate = bookStock.LoanEndDate?.AddDays(queuePosition * 7); // assuming 7 days per borrower
        var reservation = new Reservation {
            Borrower = bookStock.OnLoanTo,
            Book = bookStock.Book,
            ExpectedAvailabilityDate = expectedAvailabilityDate.Value,
        };

        context.Reservations.Add(reservation);
        context.SaveChanges();

        return ApiResponse<Guid>.SuccessResponse(reservation.Guid, $"Book reserved successfully. Your estimated availability date: {expectedAvailabilityDate?.ToShortDateString()}");
    }

    public ApiResponse<ReservationStatusDto> GetReservationStatus(Guid borrowerId, Guid bookId) {
        using var context = new LibraryContext();

        var reservation = context.Reservations.Include(r => r.Borrower)
                                              .Include(r => r.Book)
                                              .Where(r => r.Borrower.Id == borrowerId && r.Book.Id == bookId && r.IsActive)
                                              .FirstOrDefault();

        if (reservation == null)
            return ApiResponse<ReservationStatusDto>.ErrorResponse("No active reservation found for this book");

        return ApiResponse<ReservationStatusDto>.SuccessResponse(new(BorrowerName: reservation.Borrower.Name, 
                                                                     BookName: reservation.Book.Name,
                                                                     ReservationDate: reservation.ReservationDate,
                                                                     ExpectedAvailabilityDate: reservation.ExpectedAvailabilityDate), "Reservation status retrieved successfully.");
    }
}
