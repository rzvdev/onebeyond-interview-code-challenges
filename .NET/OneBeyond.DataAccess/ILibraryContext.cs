using Microsoft.EntityFrameworkCore;
using OneBeyond.Model.Entities;

namespace OneBeyond.DataAccess;

public interface ILibraryContext
{
    DbSet<Author> Authors { get; }
    DbSet<Book> Books { get; }
    DbSet<BookStock> Catalogue { get; }
    DbSet<Borrower> Borrowers { get; }
    DbSet<Fine> Fines { get; }
    DbSet<Reservation> Reservations { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
