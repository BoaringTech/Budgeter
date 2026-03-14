using Budgeter.Server.DTOs;
using Budgeter.Server.Entities;
using Budgeter.Server.Repositories.Interfaces;
using Budgeter.Server.Requests;
using Budgeter.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Budgeter.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionsController(ITransactionService transactionService, ITransactionRepository transactionRepository)
        {
            _transactionService = transactionService;
            _transactionRepository = transactionRepository;
        }

        // GET /transactions/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactions()
        {
            IEnumerable<Transaction> transactions = await _transactionRepository.GetAllTransactionsAsync();
            IEnumerable<TransactionDTO> transactionsDTO = transactions.Select(_transactionService.TranslateTransaction);
            return Ok(transactionsDTO);
        }

        // GET /transactions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDTO>> GetTransaction(int id)
        {
            Transaction? transaction = await _transactionRepository.GetTransactionByIdAsync(id);

            if (transaction == null)
                return NotFound();

            TransactionDTO transactionDTO = _transactionService.TranslateTransaction(transaction);

            return Ok(transactionDTO);
        }

        // GET /transactions/time?start={start}&end={end}
        [HttpGet("time")]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactionsByDateRange(
            [FromQuery] DateTime start,
            [FromQuery] DateTime end)
        {
            IEnumerable<Transaction> transactions = await _transactionRepository.GetTransactionsByDateRangeAsync(start, end);
            IEnumerable<TransactionDTO> transactionsDTO = transactions.Select(_transactionService.TranslateTransaction);
            return Ok(transactionsDTO);
        }

        // POST /transactions
        [HttpPost]
        public async Task<ActionResult<TransactionDTO>> CreateTransaction([FromBody]CreateTransactionRequest request)
        {
            try
            {
                Transaction transaction = await _transactionRepository.CreateTransactionAsync(request);
                TransactionDTO transactionDTO = _transactionService.TranslateTransaction(transaction);
                return CreatedAtAction(nameof(CreateTransaction), new { id = transactionDTO.Id }, transactionDTO);
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
            Transaction? transaction = await _transactionRepository.UpdateTransactionAsync(id, request);

            if (transaction == null)
                return NotFound();

            TransactionDTO transactionDTO = _transactionService.TranslateTransaction(transaction);

            return Ok(transactionDTO);
        }

        // DELETE /transactions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            bool deleted = await _transactionRepository.DeleteTransactionAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
