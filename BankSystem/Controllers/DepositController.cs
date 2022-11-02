using BankSystem.Context;
using BankSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Controllers
{
    public class DepositController : Controller
    {
        private readonly ILogger<DepositController> _logger;

        private readonly AppDbContext _dbContext;

        public DepositController(ILogger<DepositController> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        //CRUD 
        [HttpGet]
        public async Task<IActionResult> CreateDeposit()
        {
            List<Deposit> deposit = (from m in _dbContext.Deposits select m).ToList();

            return View(deposit);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeposit(Deposit deposit)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Add(deposit);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(deposit);
        }

        [HttpPost, ActionName("EditDeposit")]
        public async Task<IActionResult> EditPostDeposit(Guid? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var deposit = await _dbContext.Deposits.FirstOrDefaultAsync(s => s.Id == Id);


            if (await TryUpdateModelAsync<Deposit>(
                deposit, "", s => s.Depos, s => s.Сurrency))
            {
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(deposit);
        }

        public async Task<IActionResult> EditDeposit(Guid Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var deposit = await _dbContext.Deposits.FirstOrDefaultAsync(m => m.Id == Id);

            if (deposit == null)
            {
                return NotFound();
            }

            return View(deposit);
        }

        public async Task<IActionResult> DetailsOfDeposit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deposit = await _dbContext.Deposits.FirstOrDefaultAsync(m => m.Id == id);

            if (deposit == null)
            {
                return NotFound();
            }

            return View(deposit);
        }

        public async Task<IActionResult> DeleteDeposit(Guid id, bool? Savechangeserror = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deposit = await _dbContext.Deposits.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (deposit == null)
            {
                return NotFound();
            }

            if (Savechangeserror.GetValueOrDefault())
            {
                ViewData["DeleteError"] = "Delete failed, please try again later ... ";
            }

            return View(deposit);
        }

        [HttpPost, ActionName("DeleteDeposit")]
        public async Task<IActionResult> ConfirmDeleteDeposit(Guid id)
        {
            var deposit = await _dbContext.Deposits.FindAsync(id);

            if (deposit == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _dbContext.Deposits.Remove(deposit);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(DeleteDeposit), new { id = id, Savechangeserror = true });
            }
        }
    }
}
