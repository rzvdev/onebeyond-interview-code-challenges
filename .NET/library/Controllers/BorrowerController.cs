using Microsoft.AspNetCore.Mvc;
using OneBeyond.DomainLogic;
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
    public async Task<IActionResult> Get() {
        var response = await _borrowerRepository.GetBorrowersAsync();
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPost]
    [Route("AddBorrower")]
    public async Task<IActionResult> Post(Borrower borrower) {
        var response = await _borrowerRepository.AddBorrowerAsync(borrower);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet]
    [Route("OnLoan")]
    public async Task<IActionResult> GetBorrowersLoan() {
        // Using IActionResult allows for better API response handling.
        // This approach ensures a more restful API compared to returning a raw List<T> or IList<T>

        var response = await _borrowerRepository.GetBorrowersAsync();
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPut]
    [Route("OnLoad/Return/{bookStockId}")]
    public async Task<IActionResult> ReturnBook(Guid bookStockId) {
        var response = await _borrowerRepository.MarkBookAsReturned(bookStockId);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPost]
    [Route("ReserveBook/{borrowerId}/{bookId}")]
    public async Task<IActionResult> ReserveBook(Guid borrowerId, Guid bookId) {
        var response = await _borrowerRepository.ReserveBook(borrowerId, bookId);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet]
    [Route("ReservationStatus/{borrowerId}/{bookId}")]
    public async Task<IActionResult> GetReservationStatus(Guid borrowerId, Guid bookId) {
        var response = await _borrowerRepository.GetReservationStatus(borrowerId, bookId);
        return response.Success ? Ok(response) : BadRequest(response);
    }
}