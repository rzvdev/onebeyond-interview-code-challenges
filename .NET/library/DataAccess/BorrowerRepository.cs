using OneBeyondApi.Model;
using OneBeyondApi.Model.Core;
using OneBeyondApi.Model.DTOs;

namespace OneBeyondApi.DataAccess
{
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

            var borrowers = context.Catalogue.Where(bs => bs.OnLoanTo != null && bs.LoanEndDate > DateTime.UtcNow)
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

            var bookStock = context.Catalogue.FirstOrDefault(bs => bs.Id == bookStockId && bs.OnLoanTo != null);

            if (bookStock is null)
                return ApiResponse<Guid>.ErrorResponse("Book not found");
            if (bookStock.OnLoanTo is null)
                return ApiResponse<Guid>.ErrorResponse("Book is not currently on loan");

            // mark the book as returned
            bookStock.OnLoanTo = null;
            bookStock.LoanEndDate = null;
            context.SaveChanges();

            return ApiResponse<Guid>.SuccessResponse(bookStockId, "Book successfully returned");
        }
    }
}
