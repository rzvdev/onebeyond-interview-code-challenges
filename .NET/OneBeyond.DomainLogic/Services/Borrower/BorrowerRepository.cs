using OneBeyond.DataAccess.DAOs;
using OneBeyond.DomainLogic.Utility;
using OneBeyond.Model.Core;
using OneBeyond.Model.DTOs;
using OneBeyond.Model.Entities;

namespace OneBeyond.DomainLogic;

public class BorrowerRepository : IBorrowerRepository
{
    private readonly IBorrowerDAO _borrowerDao;
    public BorrowerRepository(IBorrowerDAO borrowerDAO)
    {
        _borrowerDao = borrowerDAO;
    }

    public async Task<ApiResponse<List<Borrower>>> GetBorrowersAsync()
    {
        var borrowers = await _borrowerDao.GetBorrowersAsync();

        return borrowers.Count != 0
            ? ApiResponse<List<Borrower>>.SuccessResponse(borrowers)
            : ApiResponse<List<Borrower>>.ErrorResponse("No borrowers found.");
    }

    public async Task<ApiResponse<Guid>> AddBorrowerAsync(Borrower borrower)
    {
        var guid = await _borrowerDao.AddBorrowerAsync(borrower);
        return ApiResponse<Guid>.SuccessResponse(guid);
    }

    public async Task<ApiResponse<List<BorrowerLoanDto>>> GetBorrowersLoansAsync() {
        var borrowers = await _borrowerDao.GetBorrowersLoansAsync();

        return borrowers.Count != 0
            ? ApiResponse<List<BorrowerLoanDto>>.SuccessResponse(borrowers, "Borrowers with active loans retrieved successfully")
            : ApiResponse<List<BorrowerLoanDto>>.ErrorResponse("No borrowers found with active loans.");
    }

    public async Task<ApiResponse<Guid>> MarkBookAsReturned(Guid bookStockId) {
        var bookStock = await _borrowerDao.GetBookStockByIdAsync(bookStockId);

        if (bookStock is null)
            return ApiResponse<Guid>.ErrorResponse("Book not found");
        if (bookStock.OnLoanTo is null)
            return ApiResponse<Guid>.ErrorResponse("Book is not currently on loan");

        // check for fine
        var returnDate = DateTime.UtcNow;
        decimal fineAmount = 0;

        if (bookStock.LoanEndDate.HasValue && returnDate > bookStock.LoanEndDate.Value) {
            fineAmount = FineCalculator.CalculateFine(bookStock.LoanEndDate.Value, returnDate);

            await _borrowerDao.AddFineAsync(new (){
                Borrower = bookStock.OnLoanTo,
                BookStock = bookStock,
                Amount = fineAmount
            });
        }

        // mark the book as returned
        bookStock.OnLoanTo = null;
        bookStock.LoanEndDate = null;
        await _borrowerDao.SaveChangesAsync();

        return fineAmount > 0 
            ? ApiResponse<Guid>.SuccessResponse(bookStockId, $"Book returned late. Fine imposed: ${fineAmount}")
            : ApiResponse<Guid>.SuccessResponse(bookStockId, "Book successfully returned");
    }

    public async Task<ApiResponse<Guid>> ReserveBook(Guid borrowerId, Guid bookId) {
        // check if book was already reserved by that borrower
        if (await _borrowerDao.IsBookReservedByBorrowerAsync(borrowerId, bookId))
            return ApiResponse<Guid>.ErrorResponse("You have already reserved that book");

        // check the queue for that book reservation
        var queuePosition = await _borrowerDao.GetReservationQueuePositionAsync(bookId);
        var expectedAvailabilityDate = DateTime.UtcNow.AddDays(queuePosition * 7);  // assuming 7 days per borrower

        var reservation = new Reservation {
            Borrower = new Borrower { Id = borrowerId },
            Book = new Book { Id = bookId },
            ExpectedAvailabilityDate = expectedAvailabilityDate
        };

        var reservationId = await _borrowerDao.AddReservationAsync(reservation);


        return ApiResponse<Guid>.SuccessResponse(reservation.Guid, $"Book reserved successfully. Your estimated availability date: {expectedAvailabilityDate.ToShortDateString()}");
    }

    public async Task<ApiResponse<ReservationStatusDto>> GetReservationStatus(Guid borrowerId, Guid bookId) {
        var reservation = await _borrowerDao.GetReservationStatusAsync(borrowerId, bookId);

        if (reservation == null)
            return ApiResponse<ReservationStatusDto>.ErrorResponse("No active reservation found for this book");

        return ApiResponse<ReservationStatusDto>.SuccessResponse(new(BorrowerName: reservation.Borrower.Name, 
                                                                     BookName: reservation.Book.Name,
                                                                     ReservationDate: reservation.ReservationDate,
                                                                     ExpectedAvailabilityDate: reservation.ExpectedAvailabilityDate), "Reservation status retrieved successfully.");
    }
}
