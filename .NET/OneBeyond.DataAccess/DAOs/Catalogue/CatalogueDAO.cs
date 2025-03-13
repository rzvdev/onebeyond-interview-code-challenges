using Microsoft.EntityFrameworkCore;
using OneBeyond.Model.Entities;

namespace OneBeyond.DataAccess.DAOs;

public sealed class CatalogueDAO : ICatalogueDAO
{
    private readonly ILibraryContext _context;

    public CatalogueDAO(ILibraryContext context) {
        _context = context;
    }

    public async Task<List<BookStock>> GetCatalogueAsync() {
        return await _context.Catalogue
                       .Include(x => x.Book)
                       .ThenInclude(x => x.Author)
                       .Include(x => x.OnLoanTo)
                       .ToListAsync();
    }

    public async Task<List<BookStock>> SearchCatalogueAsync(CatalogueSearch search) {
        var list =  _context.Catalogue
                         .Include(x => x.Book)
                         .ThenInclude(x => x.Author)
                         .Include(x => x.OnLoanTo)
                         .AsQueryable();

        if (search != null) {
            if (!string.IsNullOrEmpty(search.Author)) {
                list = list.Where(x => x.Book.Author.Name.Contains(search.Author));
            }
            if (!string.IsNullOrEmpty(search.BookName)) {
                list = list.Where(x => x.Book.Name.Contains(search.BookName));
            }
        }

        return await list.ToListAsync();
    }
}
