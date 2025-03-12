using OneBeyond.Model.Entities;

namespace OneBeyond.DataAccess;
public interface ICatalogueRepository
{
    public List<BookStock> GetCatalogue();
    public List<BookStock> SearchCatalogue(CatalogueSearch search);
}
