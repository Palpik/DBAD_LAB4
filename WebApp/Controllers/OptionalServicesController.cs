using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp;

namespace WebApp.Controllers
{
    public class OptionalServicesController : Controller
    {
        private readonly AdAgencyContext _context;

        public OptionalServicesController(AdAgencyContext context)
        {
            _context = context;
        }

        // GET: OptionalServices
        public async Task<IActionResult> Index()
        {
              return View(await _context.OptionalServices.ToListAsync());
        }

        // GET: OptionalServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OptionalServices == null)
            {
                return NotFound();
            }

            var optionalService = await _context.OptionalServices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (optionalService == null)
            {
                return NotFound();
            }

            return View(optionalService);
        }

        // GET: OptionalServices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OptionalServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Cost")] OptionalService optionalService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(optionalService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(optionalService);
        }

        // GET: OptionalServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OptionalServices == null)
            {
                return NotFound();
            }

            var optionalService = await _context.OptionalServices.FindAsync(id);
            if (optionalService == null)
            {
                return NotFound();
            }
            return View(optionalService);
        }

        // POST: OptionalServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Cost")] OptionalService optionalService)
        {
            if (id != optionalService.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(optionalService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OptionalServiceExists(optionalService.Id))
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
            return View(optionalService);
        }

        // GET: OptionalServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OptionalServices == null)
            {
                return NotFound();
            }

            var optionalService = await _context.OptionalServices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (optionalService == null)
            {
                return NotFound();
            }

            return View(optionalService);
        }

        // POST: OptionalServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OptionalServices == null)
            {
                return Problem("Entity set 'AdAgencyContext.OptionalServices'  is null.");
            }
            var optionalService = await _context.OptionalServices.FindAsync(id);
            if (optionalService != null)
            {
                _context.OptionalServices.Remove(optionalService);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OptionalServiceExists(int id)
        {
          return _context.OptionalServices.Any(e => e.Id == id);
        }
    }
}
