using BankSystem.Context;
using BankSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly ILogger<CurrencyController> _logger;

        private readonly AppDbContext _dbContext;

        public CurrencyController(ILogger<CurrencyController> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        //CRUD 
        [HttpGet]
        public async Task<IActionResult> CreateCurrency()
        {
            List<Currency> currency = (from m in _dbContext.Currencies select m).ToList();

            return View(currency);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCurrency(Currency currency)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Add(currency);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(currency);
        }

        [HttpPost, ActionName("EditCurrency")]
        public async Task<IActionResult> EditPostCurrency(Guid? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var currency = await _dbContext.Currencies.FirstOrDefaultAsync(s => s.Id == Id);


            if (await TryUpdateModelAsync<Currency>(
                currency, "", s => s.Name, s => s.ShortName, s => s.CostInKZT))
            {
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(currency);
        }

        public async Task<IActionResult> EditCurrency(Guid Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var currency = await _dbContext.Currencies.FirstOrDefaultAsync(m => m.Id == Id);

            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        public async Task<IActionResult> DetailsOfCurrency(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _dbContext.Currencies.FirstOrDefaultAsync(m => m.Id == id);

            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        public async Task<IActionResult> DeleteCurrency(Guid id, bool? Savechangeserror = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _dbContext.Currencies.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (currency == null)
            {
                return NotFound();
            }

            if (Savechangeserror.GetValueOrDefault())
            {
                ViewData["DeleteError"] = "Delete failed, please try again later ... ";
            }

            return View(currency);
        }

        [HttpPost, ActionName("DeleteCurrency")]
        public async Task<IActionResult> ConfirmDeletePerson(Guid id)
        {
            var currency = await _dbContext.Currencies.FindAsync(id);

            if (currency == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _dbContext.Currencies.Remove(currency);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(DeleteCurrency), new { id = id, Savechangeserror = true });
            }
        }
    }
}
