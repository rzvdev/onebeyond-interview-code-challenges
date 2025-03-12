using OneBeyondApi.Model;
using OneBeyondApi.Model.DTOs;

namespace OneBeyondApi.DataAccess
{
    public interface IBorrowerRepository
    {
        public List<Borrower> GetBorrowers();

        public Guid AddBorrower(Borrower borrower);

        /// <summary>
        /// Retrieves a list of BorrowerLoanDto objects which 
        /// contains details about who currently have books on loan.
        /// </summary>
        /// <returns>
        ///  A list of BorrowerLoanDto objects representing borrowers with active loans.
        ///  If no borrowers are found, returns an empty list.
        /// </returns>
        public List<BorrowerLoanDto> GetBorrowersLoans();
    }
}
