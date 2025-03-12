using OneBeyond.Model.Entities;

namespace OneBeyond.DataAccess;

public class BookRepository : IBookRepository
{
    private readonly ILibraryContext _context;

    public BookRepository(ILibraryContext context)
    {
        _context = context;
    }

    public List<Book> GetBooks()
    {
        return _context.Books
                       .ToList();
    }

    public Guid AddBook(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChangesAsync();
        
        return book.Id;
    }
}
