﻿using Microsoft.EntityFrameworkCore;
using OneBeyond.Model.Entities;

namespace OneBeyond.DataAccess;
public class CatalogueRepository : ICatalogueRepository
{
    private readonly ILibraryContext _context;
    public CatalogueRepository(ILibraryContext context)
    {
        _context = context;
    }

    public List<BookStock> GetCatalogue()
    {
        return _context.Catalogue
                       .Include(x => x.Book)
                       .ThenInclude(x => x.Author)
                       .Include(x => x.OnLoanTo)
                       .ToList();
    }

    public List<BookStock> SearchCatalogue(CatalogueSearch search)
    {
        var list = _context.Catalogue
                           .Include(x => x.Book)
                           .ThenInclude(x => x.Author)
                           .Include(x => x.OnLoanTo)
                           .AsQueryable();

        if (search != null)
        {
            if (!string.IsNullOrEmpty(search.Author)) {
                list = list.Where(x => x.Book.Author.Name.Contains(search.Author));
            }
            if (!string.IsNullOrEmpty(search.BookName)) {
                list = list.Where(x => x.Book.Name.Contains(search.BookName));
            }
        }
                
        return list.ToList();
    }
}
