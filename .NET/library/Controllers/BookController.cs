using Microsoft.AspNetCore.Mvc;
using OneBeyond.DataAccess;
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
    public IList<Book> Get() {
        return _bookRepository.GetBooks();
    }

    [HttpPost]
    [Route("AddBook")]
    public Guid Post(Book book) {
        return _bookRepository.AddBook(book);
    }
}