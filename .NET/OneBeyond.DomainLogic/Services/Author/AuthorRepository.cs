using OneBeyond.DataAccess.DAOs;
using OneBeyond.Model.Entities;

namespace OneBeyond.DomainLogic;

public class AuthorRepository : IAuthorRepository
{
    private readonly IAuthorDAO _authorDao;
    public AuthorRepository(IAuthorDAO authorDAO) {
        _authorDao = authorDAO;
    }

    public async Task<List<Author>> GetAuthorsAsync() {
        return await _authorDao.GetAuthorsAsync();
    }

    public async Task<Guid> AddAuthorAsync(Author author) {
        await _authorDao.AddAuthorAsync(author);
        return author.Id;
    }
}
