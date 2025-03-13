using OneBeyond.Model.Entities;

namespace OneBeyond.DomainLogic;
public interface IAuthorRepository
{
    public Task<List<Author>> GetAuthorsAsync();

    public Task<Guid> AddAuthorAsync(Author author);
}
