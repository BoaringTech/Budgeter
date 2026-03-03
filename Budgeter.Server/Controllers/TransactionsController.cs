using Budgeter.Server.Models;
using Budgeter.Server.Requests;
using Budgeter.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Budgeter.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // GET api/transactions/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionModel>>> GetTransactions()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();
            return Ok(transactions);
        }

        // GET api/transactions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionModel>> GetTransaction(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);

            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        // GET api/transactions/time?start={start}&end={end}
        [HttpGet("time")]
        public async Task<ActionResult<IEnumerable<TransactionModel>>> GetTransactionsByDateRange(
            [FromQuery] DateTime start,
            [FromQuery] DateTime end)
        {
            var transactions = await _transactionService.GetTransactionsByDateRangeAsync(start, end);
            return Ok(transactions);
        }

        // POST api/transactions/
        [HttpPost]
        public async Task<ActionResult<TransactionModel>> CreateTransaction(CreateTransactionRequest request)
        {
            try
            {
                var transaction = await _transactionService.CreateTransactionAsync(request);
                return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Id }, transaction);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while creating the transaction");
            }
        }

        // PUT api/transactions/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, UpdateTransactionRequest request)
        {
            TransactionModel? transaction = await _transactionService.UpdateTransactionAsync(id, request);

            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        // DELETE api/transactions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var deleted = await _transactionService.DeleteTransactionAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
