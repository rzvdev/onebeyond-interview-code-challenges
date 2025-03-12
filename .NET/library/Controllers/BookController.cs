using Microsoft.AspNetCore.Mvc;
using OneBeyond.DomainLogic;
using OneBeyond.Model.Entities;

namespace OneBeyond.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly ILogger<BookController> _logger;
    private readonly IBookRepository _bookRepository;

    public BookController(ILogger<BookController> logger, IBookRepository bookRepository) {
        _logger = logger;
        _bookRepository = bookRepository;
    }

    [HttpGet]
    [Route("GetBooks")]
    public async Task<IActionResult> Get() {
        return Ok(await _bookRepository.GetBooksAsync());
    }

    [HttpPost]
    [Route("AddBook")]
    public async Task<IActionResult> Post(Book book) {
        return Ok(await _bookRepository.AddBookAsync(book));
    }
}