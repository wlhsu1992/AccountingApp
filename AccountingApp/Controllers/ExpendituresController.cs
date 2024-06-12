using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AccountingApp.Controllers
{
    public class ExpendituresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpendituresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Expenditures
        public async Task<IActionResult> Index()
        {
              return _context.Expenditures != null ? 
                          View(await _context.Expenditures.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Expenditures'  is null.");
        }

        // GET: Expenditures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Expenditures == null)
            {
                return NotFound();
            }

            var expenditure = await _context.Expenditures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenditure == null)
            {
                return NotFound();
            }

            return View(expenditure);
        }

        // GET: Expenditures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Expenditures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Amount,CreatedTime,ExpenditureType")] Expenditure expenditure)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expenditure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expenditure);
        }

        // GET: Expenditures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Expenditures == null)
            {
                return NotFound();
            }

            var expenditure = await _context.Expenditures.FindAsync(id);
            if (expenditure == null)
            {
                return NotFound();
            }
            return View(expenditure);
        }

        // POST: Expenditures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Amount,CreatedTime,ExpenditureType")] Expenditure expenditure)
        {
            if (id != expenditure.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expenditure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenditureExists(expenditure.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(expenditure);
        }

        // GET: Expenditures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Expenditures == null)
            {
                return NotFound();
            }

            var expenditure = await _context.Expenditures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenditure == null)
            {
                return NotFound();
            }

            return View(expenditure);
        }

        // POST: Expenditures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Expenditures == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Expenditures'  is null.");
            }
            var expenditure = await _context.Expenditures.FindAsync(id);
            if (expenditure != null)
            {
                _context.Expenditures.Remove(expenditure);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenditureExists(int id)
        {
          return (_context.Expenditures?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
