using OneBeyond.DataAccess.DAOs;
using OneBeyond.Model.Entities;

namespace OneBeyond.DomainLogic;
public class CatalogueRepository : ICatalogueRepository
{
    private readonly ICatalogueDAO _catalogueDao;
    public CatalogueRepository(ICatalogueDAO catalogueDAO)
    {
        _catalogueDao = catalogueDAO;
    }

    public async Task<List<BookStock>> GetCatalogueAsync()
    {
        return await _catalogueDao.GetCatalogueAsync();
    }

    public async Task<List<BookStock>> SearchCatalogueAsync(CatalogueSearch search)
    {
        return await _catalogueDao.SearchCatalogueAsync(search);
    }
}
