using OneBeyond.Model.Entities;

namespace OneBeyond.DataAccess.DAOs;

public interface ICatalogueDAO
{
    public Task<List<BookStock>> GetCatalogueAsync();
    public Task<List<BookStock>> SearchCatalogueAsync(CatalogueSearch search);
}
