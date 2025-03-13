using OneBeyond.Model.Entities;

namespace OneBeyond.DomainLogic;
public interface ICatalogueRepository
{
    public Task<List<BookStock>> GetCatalogueAsync();
    public Task<List<BookStock>> SearchCatalogueAsync(CatalogueSearch search);
}
