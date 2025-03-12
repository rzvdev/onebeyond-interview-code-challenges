using OneBeyond.Model.DTOs;
using OneBeyond.Model.Entities;

namespace OneBeyond.DataAccess.DAOs;

public interface IBorrowerDAO
{
    /// <summary>
    /// Retrieves all borrowers from the database.
    /// </summary>
    Task<List<Borrower>> GetBorrowersAsync();

    /// <summary>
    /// Adds a new borrower to the database.
    /// </summary>
    Task<Guid> AddBorrowerAsync(Borrower borrower);

    /// <summary>
    /// Retrieves a list of borrowers who currently have books on loan.
    /// </summary>
    Task<List<BorrowerLoanDto>> GetBorrowersLoansAsync();

    /// <summary>
    /// Retrieves a specific book stock record by its unique identifier.
    /// </summary>
    Task<BookStock?> GetBookStockByIdAsync(Guid bookStockId);

    /// <summary>
    /// Adds a fine to a borrower for overdue book returns.
    /// </summary>
    Task AddFineAsync(Fine fine);

    /// <summary>
    /// Saves changes made to the database.
    /// </summary>
    Task SaveChangesAsync();

    /// <summary>
    /// Checks if a borrower has already reserved a specific book.
    /// </summary>
    Task<bool> IsBookReservedByBorrowerAsync(Guid borrowerId, Guid bookId);

    /// <summary>
    /// Retrieves the current reservation queue position for a given book.
    /// </summary>
    Task<int> GetReservationQueuePositionAsync(Guid bookId);

    /// <summary>
    /// Adds a new reservation for a borrower.
    /// </summary>
    Task<Guid> AddReservationAsync(Reservation reservation);

    /// <summary>
    /// Retrieves the reservation status of a specific borrower for a given book.
    /// </summary>
    Task<Reservation?> GetReservationStatusAsync(Guid borrowerId, Guid bookId);
}
