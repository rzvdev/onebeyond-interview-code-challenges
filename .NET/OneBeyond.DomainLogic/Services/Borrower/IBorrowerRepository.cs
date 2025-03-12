using OneBeyond.Model.Core;
using OneBeyond.Model.DTOs;
using OneBeyond.Model.Entities;

namespace OneBeyond.DomainLogic;
public interface IBorrowerRepository
{
    public Task<ApiResponse<List<Borrower>>> GetBorrowersAsync();

    public Task<ApiResponse<Guid>> AddBorrowerAsync(Borrower borrower);

    /// <summary>
    /// Retrieves a list of borrowers who currently have books on loan.
    /// </summary>
    /// <returns>
    ///  Returns an <see cref="ApiResponse{T}"/> containing a list of <see cref="BorrowerLoanDto"/>.
    /// </returns>
    public Task<ApiResponse<List<BorrowerLoanDto>>> GetBorrowersLoansAsync();

    /// <summary>
    /// Marks a book as returned, updating its loan status.
    /// </summary>
    /// <param name="bookStockId">The unique identifier (<see cref="Guid"/>) of the book stock record.</param>
    /// <returns>
    /// Returns an <see cref="ApiResponse{T}"/> with a <see cref="Guid"/> representing the returned book's stock ID.
    /// </returns>
    public Task<ApiResponse<Guid>> MarkBookAsReturned(Guid bookStockId);

    /// <summary>
    /// Reserve a book for a borrower
    /// </summary>
    /// <param name="borrowerId">The unique identifier (<see cref="Guid"/>) of the borrower record</param>
    /// <param name="bookId">The unique identifier (<see cref="Guid"/>) of the book record</param>
    /// <returns>Returns an <see cref="ApiResponse{T}"/> with a <see cref="Guid"/> representing the reservation id.</returns>
    public Task<ApiResponse<Guid>> ReserveBook(Guid borrowerId, Guid bookId);

    /// <summary>
    /// Get a reservation status 
    /// </summary>
    /// <param name="borrowerId">The unique identifier (<see cref="Guid"/>) of the borrower record</param>
    /// <param name="bookId">The unique identifier (<see cref="Guid"/>) of the book record></param>
    /// <returns>Returns an <see cref="ApiResponse{T}"/> with a <see cref="ReservationStatusDto"/> representing the reservation status</returns>
    public Task<ApiResponse<ReservationStatusDto>> GetReservationStatus(Guid borrowerId, Guid bookId);
}
