using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Azure.Documents;
using Microsoft.Build.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Recipes.Data;
using Recipes.Models;


namespace Recipes.Controllers
{
    [Authorize]
    public class TorteController : Controller
    {
    
        private readonly ApplicationDbContext _context;

        public TorteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Torte
       
        public async Task<IActionResult> Index()
        {
            return View(await _context.Torte.ToListAsync());
        }

         

        // GET: Torte/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var torte = await _context.Torte
                .FirstOrDefaultAsync(m => m.Id == id);
            if (torte == null)
            {
                return NotFound();
            }

            return View(torte);
        }

        // GET: Torte/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Torte/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Create([Bind("Id,Name,Ingredients,Preparation")] Torte torte)
        {
            if (ModelState.IsValid)
            {
                _context.Add(torte);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(torte);


            if (User.HasClaim(c => c.Type == "Permission" && c.Value == "CanEdit"))
            {
                // Logic for users who can edit
            }
            else
            {
                // Access denied or show a message
            }

            return View();
        }

        // GET: Torte/Edit/5
     
        [Authorize(Roles = "Admin")]
        
        public async Task<IActionResult> Edit(int? id)
        {
       

            var torte = await _context.Torte.FindAsync(id);
            if (torte == null)
            {
                return NotFound();
            }
            return View(torte);

            if (User.IsInRole("Admin"))
            {
                // Logic for Admin users
            }
            else
            {
                // Logic for non-admin users
                return View("Error"); // You can redirect to an error page or show a message.
            }
            return View();

        }
    

    // POST: Torte/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Ingredients,Preparation")] Torte torte)
        {
            if (id != torte.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(torte);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TorteExists(torte.Id))
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
            return View(torte);

            

        }

        

        // GET: Torte/Delete/5

        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var torte = await _context.Torte
                .FirstOrDefaultAsync(m => m.Id == id);
            if (torte == null)
            {
                return NotFound();
            }

            return View(torte);
        }

        // POST: Torte/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> DeleteConfirmed(int id)
        { 
            var torte = await _context.Torte.FindAsync(id);
            if (torte != null)
            {
                _context.Torte.Remove(torte);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

         
        }

        private bool TorteExists(int id)
        {
            return _context.Torte.Any(e => e.Id == id);
        }



    }
}
