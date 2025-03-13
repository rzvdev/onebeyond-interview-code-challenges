using OneBeyond.Model.Entities;

namespace OneBeyond.DataAccess.DAOs;

public interface IAuthorDAO
{
    public Task<List<Author>> GetAuthorsAsync();

    public Task<Guid> AddAuthorAsync(Author author);
}
