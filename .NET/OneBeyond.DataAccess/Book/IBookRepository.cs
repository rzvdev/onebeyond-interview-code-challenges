using OneBeyond.Model.Entities;

namespace OneBeyond.DataAccess;

public interface IBookRepository
{
    public List<Book> GetBooks();

    public Guid AddBook(Book book);
}
