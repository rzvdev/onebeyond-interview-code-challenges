using OneBeyond.Model.Entities;

namespace OneBeyond.DataAccess.DAOs;

public interface IBookDao
{
    /// <summary>
    /// Retrieves all books from the database.
    /// </summary>
    Task<List<Book>> GetBooksAsync();

    /// <summary>
    /// Adds a new book to the database.
    /// </summary>
    Task<Guid> AddBookAsync(Book book);
}
