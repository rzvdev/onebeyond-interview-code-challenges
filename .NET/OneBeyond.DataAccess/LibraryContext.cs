using Microsoft.EntityFrameworkCore;
using OneBeyond.Model.Entities;

namespace OneBeyond.DataAccess;

public class LibraryContext(DbContextOptions<LibraryContext> options) : DbContext(options), ILibraryContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookStock> Catalogue { get; set; }
    public DbSet<Borrower> Borrowers { get; set; }
    public DbSet<Fine> Fines { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
}
