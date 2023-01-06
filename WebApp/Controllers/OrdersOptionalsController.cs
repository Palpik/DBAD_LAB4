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
    public class OrdersOptionalsController : Controller
    {
        private readonly AdAgencyContext _context;

        public OrdersOptionalsController(AdAgencyContext context)
        {
            _context = context;
        }

        // GET: OrdersOptionals
        public async Task<IActionResult> Index()
        {
            var adAgencyContext = _context.OrdersOptionals.Include(o => o.Option).Include(o => o.Order);
            return View(await adAgencyContext.ToListAsync());
        }

        // GET: OrdersOptionals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrdersOptionals == null)
            {
                return NotFound();
            }

            var ordersOptional = await _context.OrdersOptionals
                .Include(o => o.Option)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ordersOptional == null)
            {
                return NotFound();
            }

            return View(ordersOptional);
        }

        // GET: OrdersOptionals/Create
        public IActionResult Create()
        {
            ViewData["OptionId"] = new SelectList(_context.OptionalServices, "Id", "Id");
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id");
            return View();
        }

        // POST: OrdersOptionals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OptionId,OrderId")] OrdersOptional ordersOptional)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordersOptional);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OptionId"] = new SelectList(_context.OptionalServices, "Id", "Id", ordersOptional.OptionId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", ordersOptional.OrderId);
            return View(ordersOptional);
        }

        // GET: OrdersOptionals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrdersOptionals == null)
            {
                return NotFound();
            }

            var ordersOptional = await _context.OrdersOptionals.FindAsync(id);
            if (ordersOptional == null)
            {
                return NotFound();
            }
            ViewData["OptionId"] = new SelectList(_context.OptionalServices, "Id", "Id", ordersOptional.OptionId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", ordersOptional.OrderId);
            return View(ordersOptional);
        }

        // POST: OrdersOptionals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OptionId,OrderId")] OrdersOptional ordersOptional)
        {
            if (id != ordersOptional.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordersOptional);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersOptionalExists(ordersOptional.Id))
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
            ViewData["OptionId"] = new SelectList(_context.OptionalServices, "Id", "Id", ordersOptional.OptionId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", ordersOptional.OrderId);
            return View(ordersOptional);
        }

        // GET: OrdersOptionals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrdersOptionals == null)
            {
                return NotFound();
            }

            var ordersOptional = await _context.OrdersOptionals
                .Include(o => o.Option)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ordersOptional == null)
            {
                return NotFound();
            }

            return View(ordersOptional);
        }

        // POST: OrdersOptionals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrdersOptionals == null)
            {
                return Problem("Entity set 'AdAgencyContext.OrdersOptionals'  is null.");
            }
            var ordersOptional = await _context.OrdersOptionals.FindAsync(id);
            if (ordersOptional != null)
            {
                _context.OrdersOptionals.Remove(ordersOptional);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersOptionalExists(int id)
        {
          return _context.OrdersOptionals.Any(e => e.Id == id);
        }
    }
}
