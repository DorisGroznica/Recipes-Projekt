using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recipes.Data;
using Recipes.Models;

namespace Recipes.Controllers
{
    [Authorize]
    public class KremastiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KremastiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kremasti
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kremastikolači.ToListAsync());
        }

        // GET: Kremasti/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kremasti = await _context.Kremastikolači
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kremasti == null)
            {
                return NotFound();
            }

            return View(kremasti);
        }

        // GET: Kremasti/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kremasti/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Ingredients,Preparation")] Kremasti kremasti)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kremasti);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kremasti);
        }

        // GET: Kremasti/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kremasti = await _context.Kremastikolači.FindAsync(id);
            if (kremasti == null)
            {
                return NotFound();
            }
            return View(kremasti);
        }

        // POST: Kremasti/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Ingredients,Preparation")] Kremasti kremasti)
        {
            if (id != kremasti.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kremasti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KremastiExists(kremasti.Id))
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
            return View(kremasti);
        }

        // GET: Kremasti/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kremasti = await _context.Kremastikolači
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kremasti == null)
            {
                return NotFound();
            }

            return View(kremasti);
        }

        // POST: Kremasti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kremasti = await _context.Kremastikolači.FindAsync(id);
            if (kremasti != null)
            {
                _context.Kremastikolači.Remove(kremasti);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KremastiExists(int id)
        {
            return _context.Kremastikolači.Any(e => e.Id == id);
        }
    }
}
