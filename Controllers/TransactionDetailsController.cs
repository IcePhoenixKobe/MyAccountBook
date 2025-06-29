using MyAccountBook.Models;
using MyAccountBook.Services;
using Microsoft.AspNetCore.Mvc;

namespace MyAccountBook.Controllers;

public class TransactionDetailsController : Controller
{
    private readonly ILogger<TransactionDetailsController> _logger;
    private readonly AccountBookService _accountBookService;

    public TransactionDetailsController(AccountBookService accountBookService, ILogger<TransactionDetailsController> logger)
    {
        _accountBookService = accountBookService;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Details(int id)
    {
        // Here you would typically fetch the transaction details from a database
        // For now, we will return a simple view with the transaction ID
        ViewBag.TransactionId = id;
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetTransactions()
    {
        try
        {
            var transactions = await _accountBookService.GetTransactionsAsync();
            return Json(transactions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching transactions");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddTransaction([FromForm] TransactionDetails model)
    {
        try
        {
            if (model == null)
            {
                return BadRequest("Transaction details cannot be null");
            }
            else if (string.IsNullOrEmpty(model.Note))
            {
                model.Note = ""; // Default to empty string if note is not provided
            }
            await _accountBookService.AddTransactionAsync(model);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding transaction");
            return StatusCode(500, "Internal server error");
        }
    }
}