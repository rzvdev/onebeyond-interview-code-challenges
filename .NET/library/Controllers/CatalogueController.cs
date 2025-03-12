using Microsoft.AspNetCore.Mvc;
using OneBeyond.DomainLogic;
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
    public async Task<IActionResult> Get() {
        return Ok(await _catalogueRepository.GetCatalogueAsync());
    }

    [HttpPost]
    [Route("SearchCatalogue")]
    public async Task<IActionResult> Post(CatalogueSearch search) {
        return Ok(await _catalogueRepository.SearchCatalogueAsync(search));
    }
}