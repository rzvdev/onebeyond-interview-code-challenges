using Microsoft.AspNetCore.Mvc;
using OneBeyond.DomainLogic;
using OneBeyond.Model.Entities;

namespace OneBeyond.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
    private readonly ILogger<AuthorController> _logger;
    private readonly IAuthorRepository _authorRepository;

    public AuthorController(ILogger<AuthorController> logger, IAuthorRepository authorRepository) {
        _logger = logger;
        _authorRepository = authorRepository;
    }

    [HttpGet]
    [Route("GetAuthors")]
    public async Task<IActionResult> Get() {
        return Ok(await _authorRepository.GetAuthorsAsync());
    }

    [HttpPost]
    [Route("AddAuthor")]
    public async Task<IActionResult> Post(Author author) {
        return Ok(await _authorRepository.AddAuthorAsync(author));
    }
}