using Microsoft.EntityFrameworkCore;
using OneBeyond.Model.Entities;

namespace OneBeyond.DataAccess.DAOs;

public sealed class AuthorDAO : IAuthorDAO
{
    private readonly ILibraryContext _context;
    public AuthorDAO(ILibraryContext context) {
        _context = context;
    }

    public async Task<Guid> AddAuthorAsync(Author author) {
        await _context.Authors.AddAsync(author);
        await _context.SaveChangesAsync();
        return author.Id;
    }

    public async Task<List<Author>> GetAuthorsAsync() {
        return await _context.Authors.ToListAsync();
    }
}
