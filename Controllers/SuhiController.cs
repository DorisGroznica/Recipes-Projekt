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
    public class SuhiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SuhiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Suhi
        public async Task<IActionResult> Index()
        {
            return View(await _context.Suhikolači.ToListAsync());
        }

        // GET: Suhi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suhi = await _context.Suhikolači
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suhi == null)
            {
                return NotFound();
            }

            return View(suhi);
        }

        // GET: Suhi/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suhi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Ingredients,Preparation")] Suhi suhi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(suhi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(suhi);
        }

        // GET: Suhi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suhi = await _context.Suhikolači.FindAsync(id);
            if (suhi == null)
            {
                return NotFound();
            }
            return View(suhi);
        }

        // POST: Suhi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Ingredients,Preparation")] Suhi suhi)
        {
            if (id != suhi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suhi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuhiExists(suhi.Id))
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
            return View(suhi);
        }

        // GET: Suhi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suhi = await _context.Suhikolači
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suhi == null)
            {
                return NotFound();
            }

            return View(suhi);
        }

        // POST: Suhi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var suhi = await _context.Suhikolači.FindAsync(id);
            if (suhi != null)
            {
                _context.Suhikolači.Remove(suhi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuhiExists(int id)
        {
            return _context.Suhikolači.Any(e => e.Id == id);
        }
    }
}
