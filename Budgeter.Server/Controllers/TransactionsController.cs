using Azure.Core;
using Budgeter.Server.DTOs;
using Budgeter.Server.Requests;
using Budgeter.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Budgeter.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IBudgetRepository _transactionService;

        public TransactionsController(IBudgetRepository transactionService)
        {
            _transactionService = transactionService;
        }

        // GET /transactions/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactions()
        {
            IEnumerable<TransactionDTO> transactions = await _transactionService.GetAllTransactionsAsync();
            return Ok(transactions);
        }

        // GET /transactions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDTO>> GetTransaction(int id)
        {
            TransactionDTO? transaction = await _transactionService.GetTransactionByIdAsync(id);

            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        // GET /transactions/time?start={start}&end={end}
        [HttpGet("time")]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactionsByDateRange(
            [FromQuery] DateTime start,
            [FromQuery] DateTime end)
        {
            var transactions = await _transactionService.GetTransactionsByDateRangeAsync(start, end);
            return Ok(transactions);
        }

        // POST /transactions
        [HttpPost]
        public async Task<ActionResult<TransactionDTO>> CreateTransaction([FromBody]CreateTransactionRequest request)
        {
            try
            {
                TransactionDTO transaction = await _transactionService.CreateTransactionAsync(request);
                return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Id }, transaction);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while creating the transaction");
            }
        }

        // PUT /transactions/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, UpdateTransactionRequest request)
        {
            TransactionDTO? transaction = await _transactionService.UpdateTransactionAsync(id, request);

            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        // DELETE /transactions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            bool deleted = await _transactionService.DeleteTransactionAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
