using OneBeyond.DataAccess.DAOs;
using OneBeyond.Model.Entities;

namespace OneBeyond.DomainLogic;

public class BookRepository : IBookRepository
{
    private readonly IBookDao _bookDao;

    public BookRepository(IBookDao bookDao)
    {
        _bookDao = bookDao;
    }

    public async Task<List<Book>> GetBooksAsync()
    {
        return await _bookDao.GetBooksAsync();
    }

    public async Task<Guid> AddBookAsync(Book book)
    {
        await _bookDao.AddBookAsync(book);
        return book.Id;
    }
}
