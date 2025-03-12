using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;

namespace OneBeyondApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BorrowerController : ControllerBase
    {
        private readonly ILogger<BorrowerController> _logger;
        private readonly IBorrowerRepository _borrowerRepository;

        public BorrowerController(ILogger<BorrowerController> logger, IBorrowerRepository borrowerRepository)
        {
            _logger = logger;
            _borrowerRepository = borrowerRepository;   
        }

        [HttpGet]
        [Route("GetBorrowers")]
        public IList<Borrower> Get()
        {
            return _borrowerRepository.GetBorrowers();
        }

        [HttpPost]
        [Route("AddBorrower")]
        public Guid Post(Borrower borrower)
        {
            return _borrowerRepository.AddBorrower(borrower);
        }

        /// <summary>
        /// Retrieves a list of borrowers who currently have books on loan.
        /// </summary>
        /// <returns>
        ///  IActionResult containing a list of borrowers with active loans,
        ///  or a NotFound response if no borrowers are found
        /// </returns>
        [HttpGet]
        [Route("OnLoan")]
        public IActionResult GetBorrowersLoan() {
            // Using IActionResult allows for better API response handling.
            // - If no borrowers have active loans, we return 404 Not Found.
            // - If borrowers exists, we return 200 OK with the data.
            // This approach ensures a more restful API compared to returning a raw List<T> or IList<T>

            var borrowers = _borrowerRepository.GetBorrowersLoans();
            if (borrowers == null || borrowers.Count == 0)
                return NotFound("No borrowers found.");

            return Ok(borrowers);
        }
    }
}