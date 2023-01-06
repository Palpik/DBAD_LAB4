using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp;
using WebApp.ViewModels.IndexViewModels;
using WebApp.ViewModels.PageViewModels;
using WebApp.ViewModels.SortViewModels;
using WebApp.Enum;
using WebApp.ViewModels.FilterViewModels;

namespace WebApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AdAgencyContext _context;
        private readonly int _pageSize = 12;

        public OrdersController(AdAgencyContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page = 1, SortState sortOrder = SortState.NumberAsc)
        {
            IEnumerable<Order> orders = _context.Orders.Include(o => o.Place).Include(o => o.Customer).Include(o => o.Employee).ToList();

            switch(sortOrder)
            {
                case SortState.PlaceDesc:
                    orders = orders.OrderByDescending(f => f.Place);
                    break;
                case SortState.PlaceAsc:
                    orders = orders.OrderBy(f => f.Place);
                    break;
                case SortState.CostDesc:
                    orders = orders.OrderByDescending(f => f.Place.Cost);
                    break;
                case SortState.CostAsc:
                    orders = orders.OrderBy(f => f.Place.Cost);
                    break;
                case SortState.AcceptDateDesc:
                    orders = orders.OrderByDescending(f => f.OrderDate);
                    break;
                case SortState.AcceptDateAsc:
                    orders = orders.OrderBy(f => f.OrderDate);
                    break;
                case SortState.StartDateDesc:
                    orders = orders.OrderByDescending(f => f.StartDate);
                    break;
                case SortState.StartDateAsc:
                    orders = orders.OrderBy(f => f.StartDate);
                    break;
                case SortState.EndDateDesc:
                    orders = orders.OrderByDescending(f => f.EndDate);
                    break;
                case SortState.EndDateAsc:
                    orders = orders.OrderBy(f => f.EndDate);
                    break;
                case SortState.NumberDesc:
                    orders = orders.OrderByDescending(f => f.Id);
                    break;
                default:
                    orders = orders.OrderBy(f => f.Id);
                    break;
            }
            
            var count = orders.Count();
            var items = orders.Skip((page - 1) * _pageSize).Take(_pageSize).ToList();
            
            OrdersViewModel viewModel = new OrdersViewModel(items,
                new PageViewModel(count,page,_pageSize),
                new OrderSortViewModel(sortOrder)
            );
            return View(viewModel);
        }
        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.Place)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(
                from s in _context.Customers.ToList() select new {
                    Id = s.Id,
                    FullName = s.LastName + " " + s.FirstName
                }, "Id", "FullName");
            ViewData["EmployeeId"] = new SelectList(
                from s in _context.Employees.ToList() select new {
                    Id = s.Id,
                    FullName = s.LastName + " " + s.FirstName
                }, "Id", "FullName");
            ViewData["PlaceId"] = new SelectList(_context.AdPlaces, "Id", "Place");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderDate,StartDate,EndDate,CustomerId,PlaceId,EmployeeId")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(
                from s in _context.Customers.ToList() select new {
                    Id = s.Id,
                    FullName = s.LastName + " " + s.FirstName
                }, "Id", "FullName");
            ViewData["EmployeeId"] = new SelectList(
                from s in _context.Employees.ToList() select new {
                    Id = s.Id,
                    FullName = s.LastName + " " + s.FirstName
                }, "Id", "FullName");
            ViewData["PlaceId"] = new SelectList(_context.AdPlaces, "Id", "Place");
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(
                from s in _context.Customers.ToList() select new {
                    Id = s.Id,
                    FullName = s.LastName + " " + s.FirstName
                }, "Id", "FullName");
            ViewData["EmployeeId"] = new SelectList(
                from s in _context.Employees.ToList() select new {
                    Id = s.Id,
                    FullName = s.LastName + " " + s.FirstName
                }, "Id", "FullName");
            ViewData["PlaceId"] = new SelectList(_context.AdPlaces, "Id", "Place");
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderDate,StartDate,EndDate,CustomerId,PlaceId,EmployeeId")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["CustomerId"] = new SelectList(
                from s in _context.Customers.ToList() select new {
                    Id = s.Id,
                    FullName = s.LastName + " " + s.FirstName
                }, "Id", "FullName");
            ViewData["EmployeeId"] = new SelectList(
                from s in _context.Employees.ToList() select new {
                    Id = s.Id,
                    FullName = s.LastName + " " + s.FirstName
                }, "Id", "FullName");
            ViewData["PlaceId"] = new SelectList(_context.AdPlaces, "Id", "Place");
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.Place)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'AdAgencyContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return _context.Orders.Any(e => e.Id == id);
        }
    }
}
