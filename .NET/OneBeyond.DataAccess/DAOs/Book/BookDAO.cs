using Microsoft.EntityFrameworkCore;
using OneBeyond.Model.Entities;

namespace OneBeyond.DataAccess.DAOs;

public sealed class BookDao : IBookDao
{
    private readonly LibraryContext _libraryContext;

    public BookDao(LibraryContext libraryContext) {
        _libraryContext = libraryContext;
    }

    public async Task<Guid> AddBookAsync(Book book) {
        await _libraryContext.Books.AddAsync(book);
        await _libraryContext.SaveChangesAsync();
        return book.Id;
    }

    public async Task<List<Book>> GetBooksAsync() {
        return await _libraryContext.Books.ToListAsync();
    }
}
