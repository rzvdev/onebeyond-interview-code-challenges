using Microsoft.AspNetCore.Mvc;
using OneBeyond.DataAccess;
using OneBeyond.Model.Entities;

namespace OneBeyond.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BorrowerController : ControllerBase
{
    private readonly ILogger<BorrowerController> _logger;
    private readonly IBorrowerRepository _borrowerRepository;

    public BorrowerController(ILogger<BorrowerController> logger, IBorrowerRepository borrowerRepository) {
        _logger = logger;
        _borrowerRepository = borrowerRepository;
    }

    [HttpGet]
    [Route("GetBorrowers")]
    public IList<Borrower> Get() {
        return _borrowerRepository.GetBorrowers();
    }

    [HttpPost]
    [Route("AddBorrower")]
    public Guid Post(Borrower borrower) {
        return _borrowerRepository.AddBorrower(borrower);
    }

    [HttpGet]
    [Route("OnLoan")]
    public IActionResult GetBorrowersLoan() {
        // Using IActionResult allows for better API response handling.
        // - If no borrowers have active loans, we return 404 Not Found.
        // - If borrowers exists, we return 200 OK with the data.
        // This approach ensures a more restful API compared to returning a raw List<T> or IList<T>

        var response = _borrowerRepository.GetBorrowersLoans();
        return Ok(response);
    }

    [HttpPut]
    [Route("OnLoad/Return/{bookStockId}")]
    public IActionResult ReturnBook(Guid bookStockId) {
        var response = _borrowerRepository.MarkBookAsReturned(bookStockId);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPost]
    [Route("ReserveBook/{borrowerId}/{bookId}")]
    public IActionResult ReserveBook(Guid borrowerId, Guid bookId) {
        var response = _borrowerRepository.ReserveBook(borrowerId, bookId);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet]
    [Route("ReservationStatus/{borrowerId}/{bookId}")]
    public IActionResult GetReservationStatus(Guid borrowerId, Guid bookId) {
        var response = _borrowerRepository.GetReservationStatus(borrowerId, bookId);
        return response.Success ? Ok(response) : NotFound(response);
    }
}