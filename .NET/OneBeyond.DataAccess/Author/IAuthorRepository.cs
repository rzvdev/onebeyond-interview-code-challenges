using OneBeyond.Model.Entities;

namespace OneBeyond.DataAccess;
public interface IAuthorRepository
{
    public List<Author> GetAuthors();

    public Guid AddAuthor(Author author);
}
