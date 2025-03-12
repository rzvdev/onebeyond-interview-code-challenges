using OneBeyondApi.Model;
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

        public List<BorrowerLoanDto> GetBorrowersLoans() {
            using var context = new LibraryContext();

            return context.Catalogue.Where(bs => bs.OnLoanTo != null && bs.LoanEndDate > DateTime.UtcNow)
                                    .AsEnumerable()
                                    .Select(bs => new BorrowerLoanDto(
                                        BorrowerName: bs.OnLoanTo!.Name,
                                        BorrowerEmail: bs.OnLoanTo!.EmailAddress,
                                        BookTitle: bs.Book.Name,
                                        LoadEndDate: bs.LoanEndDate
                                    )).ToList();
        }
    }
}
