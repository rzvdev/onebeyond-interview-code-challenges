using OneBeyond.Model.Entities;

namespace OneBeyond.DomainLogic;

public interface IBookRepository
{
    public Task<List<Book>> GetBooksAsync();

    public Task<Guid> AddBookAsync(Book book);
}
