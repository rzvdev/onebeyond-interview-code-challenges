using Microsoft.EntityFrameworkCore;
using OneBeyond.Model.DTOs;
using OneBeyond.Model.Entities;

namespace OneBeyond.DataAccess.DAOs;

public class BorrowerDAO : IBorrowerDAO
{
    private readonly ILibraryContext _context;
    public BorrowerDAO(ILibraryContext context) {
        _context = context;
    }

    public async Task<List<Borrower>> GetBorrowersAsync() {
        return await _context.Borrowers.ToListAsync();
    }

    public async Task<Guid> AddBorrowerAsync(Borrower borrower) {
        await _context.Borrowers.AddAsync(borrower);
        await _context.SaveChangesAsync();
        return borrower.Id;
    }

    public async Task AddFineAsync(Fine fine) {
        await _context.Fines.AddAsync(fine);
        await _context.SaveChangesAsync();
    }

    public async Task<Guid> AddReservationAsync(Reservation reservation) {
        await _context.Reservations.AddAsync(reservation);
        await _context.SaveChangesAsync();
        return reservation.Guid;
    }

    public async Task<BookStock?> GetBookStockByIdAsync(Guid bookStockId) {
        return await _context.Catalogue
            .Include(bs => bs.OnLoanTo)
            .FirstOrDefaultAsync(bs => bs.Id == bookStockId);
    }

    public async Task<List<BorrowerLoanDto>> GetBorrowersLoansAsync() {
        return await _context.Catalogue
            .Where(bs => bs.OnLoanTo != null)
            .Include(bs => bs.OnLoanTo)
            .Include(bs => bs.Book)
            .Select(bs => new BorrowerLoanDto(
                bs.OnLoanTo!.Name,
                bs.OnLoanTo!.EmailAddress,
                bs.Book.Name,
                bs.LoanEndDate
            )).ToListAsync();
    }

    public async Task<int> GetReservationQueuePositionAsync(Guid bookId) {
        return await _context.Reservations
            .Where(r => r.Book.Id == bookId && r.IsActive)
            .CountAsync() + 1;
    }

    public async Task<Reservation?> GetReservationStatusAsync(Guid borrowerId, Guid bookId) {
        return await _context.Reservations
            .Include(r => r.Borrower)
            .Include(r => r.Book)
            .FirstOrDefaultAsync(r => r.Borrower.Id == borrowerId && r.Book.Id == bookId && r.IsActive);
    }

    public async Task<bool> IsBookReservedByBorrowerAsync(Guid borrowerId, Guid bookId) {
        return await _context.Reservations
           .AnyAsync(r => r.Borrower.Id == borrowerId && r.Book.Id == bookId && r.IsActive);
    }

    public async Task SaveChangesAsync() {
        await _context.SaveChangesAsync();
    }
}
