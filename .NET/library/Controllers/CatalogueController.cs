using Microsoft.AspNetCore.Mvc;
using OneBeyond.DataAccess;
using OneBeyond.Model.Entities;

namespace OneBeyond.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CatalogueController : ControllerBase
{
    private readonly ILogger<CatalogueController> _logger;
    private readonly ICatalogueRepository _catalogueRepository;

    public CatalogueController(ILogger<CatalogueController> logger, ICatalogueRepository catalogueRepository) {
        _logger = logger;
        _catalogueRepository = catalogueRepository;
    }

    [HttpGet]
    [Route("GetCatalogue")]
    public IList<BookStock> Get() {
        return _catalogueRepository.GetCatalogue();
    }

    [HttpPost]
    [Route("SearchCatalogue")]
    public IList<BookStock> Post(CatalogueSearch search) {
        return _catalogueRepository.SearchCatalogue(search);
    }
}