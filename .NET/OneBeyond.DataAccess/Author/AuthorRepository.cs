using OneBeyond.Model.Entities;

namespace OneBeyond.DataAccess;

public class AuthorRepository : IAuthorRepository
{
    private readonly ILibraryContext _libraryContext;
    public AuthorRepository(ILibraryContext libraryContext) {
        _libraryContext = libraryContext;
    }

    public List<Author> GetAuthors() {
        return [.. _libraryContext.Authors];
    }

    public Guid AddAuthor(Author author) {
        _libraryContext.Authors.Add(author);
        _libraryContext.SaveChangesAsync();
        return author.Id;
    }
}
