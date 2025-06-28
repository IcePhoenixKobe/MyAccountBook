using MyAccountBook.Models;
using MyAccountBook.Services;
using Microsoft.AspNetCore.Mvc;

namespace MyAccountBook.Controllers;
public class ItemCategoryController : Controller
{
    private readonly ILogger<ItemCategoryController> _logger;
    private readonly AccountBookService _accountBookService;

    public ItemCategoryController(AccountBookService accountBookService, ILogger<ItemCategoryController> logger)
    {
        _accountBookService = accountBookService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetItemCategories()
    {
        try
        {
            var categories = await _accountBookService.GetItemCategoriesAsync();
            return Json(categories);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching item categories");
            return StatusCode(500, "Internal server error");
        }
    }
}