using OneBeyondApi.Model;
using OneBeyondApi.Model.Core;
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
        /// <summary>
        /// Marks a book as returned, updating its loan status.
        /// </summary>
        /// <param name="bookStockId">The unique identifier (<see cref="Guid"/>) of the book stock record.</param>
        /// <returns>
        /// Returns an <see cref="ApiResponse{T}"/> with a <see cref="Guid"/> representing the returned book's stock ID.
        /// </returns>
        public ApiResponse<Guid> MarkBookAsReturned(Guid bookStockId);
    }
}
