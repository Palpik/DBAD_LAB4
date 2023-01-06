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
    public class AdPlacesController : Controller
    {
        private readonly AdAgencyContext _context;
        private readonly int _pageSize = 12;

        private string _http_string_adPlaceName = "AdPlaceName";

        private string _http_string_adPlaceType = "AdPlaceType";
        public AdPlacesController(AdAgencyContext context)
        {
            _context = context;
        }

        // GET: AdPlaces
        public IActionResult Index(string? adPlaceName, string? adPlaceType, int page = 1, SortState sortOrder = SortState.NumberAsc)
        {
            IEnumerable<AdPlace> adPlaces = _context.AdPlaces.Include(o => o.Type).ToList();
            
            
            if (!string.IsNullOrEmpty(adPlaceName))
            {
                adPlaces = adPlaces.Where(f => f.Place!.Contains(adPlaceName));
                Response.Cookies.Append(_http_string_adPlaceName, adPlaceName);
            }
            else
            {
                if (Request.Cookies.ContainsKey(_http_string_adPlaceName))
                {
                    adPlaceName = Request.Cookies[_http_string_adPlaceName];
                    Response.Cookies.Delete(_http_string_adPlaceName);
                }
            }

            if (!string.IsNullOrEmpty(adPlaceType))
            {
                adPlaces = adPlaces.Where(f => f.Type.Name!.Contains(adPlaceType));
                Response.Cookies.Append(_http_string_adPlaceType, adPlaceType);
            }
            else
            {
                if (Request.Cookies.ContainsKey(_http_string_adPlaceType))
                {
                    adPlaceType = Request.Cookies[_http_string_adPlaceType];
                    Response.Cookies.Delete(_http_string_adPlaceType);
                }
            }

            switch(sortOrder)
            {
                case SortState.PlaceDesc:
                    adPlaces = adPlaces.OrderByDescending(f => f.Place);
                    break;
                case SortState.PlaceAsc:
                    adPlaces = adPlaces.OrderBy(f => f.Place);
                    break;
                case SortState.CostDesc:
                    adPlaces = adPlaces.OrderByDescending(f => f.Cost);
                    break;
                case SortState.CostAsc:
                    adPlaces = adPlaces.OrderBy(f => f.Cost);
                    break;
                case SortState.TypeDesc:
                    adPlaces = adPlaces.OrderByDescending(f => f.Type.Name);
                    break;
                case SortState.TypeAsc:
                    adPlaces = adPlaces.OrderBy(f => f.Type.Name);
                    break;
                case SortState.NumberDesc:
                    adPlaces = adPlaces.OrderByDescending(f => f.Id);
                    break;
                default:
                    adPlaces = adPlaces.OrderBy(f => f.Id);
                    break;
            }
            
            var count = adPlaces.Count();
            var items = adPlaces.Skip((page - 1) * _pageSize).Take(_pageSize).ToList();
            
            AdPlacesViewModel viewModel = new AdPlacesViewModel(items,
                new PageViewModel(count,page,_pageSize),
                new AdPlaceSotrViewModel(sortOrder),
                new AdPlaceFilterViewModel(adPlaceName, adPlaceType)
            );
            return View(viewModel);
        }

        // GET: AdPlaces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AdPlaces == null)
            {
                return NotFound();
            }

            var adPlace = await _context.AdPlaces
                .Include(a => a.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adPlace == null)
            {
                return NotFound();
            }

            return View(adPlace);
        }

        // GET: AdPlaces/Create
        public IActionResult Create()
        {
            ViewData["TypeId"] = new SelectList(_context.AdTypes, "Id", "Name");
            return View();
        }

        // POST: AdPlaces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Place,TypeId,Cost,Description")] AdPlace adPlace)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adPlace);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeId"] = new SelectList(_context.AdTypes, "Id", "Name", adPlace.TypeId);
            return View(adPlace);
        }

        // GET: AdPlaces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AdPlaces == null)
            {
                return NotFound();
            }

            var adPlace = await _context.AdPlaces.FindAsync(id);
            if (adPlace == null)
            {
                return NotFound();
            }
            ViewData["TypeId"] = new SelectList(_context.AdTypes, "Id", "Name", adPlace.TypeId);
            return View(adPlace);
        }

        // POST: AdPlaces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Place,TypeId,Cost,Description")] AdPlace adPlace)
        {
            if (id != adPlace.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adPlace);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdPlaceExists(adPlace.Id))
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
            ViewData["TypeId"] = new SelectList(_context.AdTypes, "Id", "Name", adPlace.TypeId);
            return View(adPlace);
        }

        // GET: AdPlaces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AdPlaces == null)
            {
                return NotFound();
            }

            var adPlace = await _context.AdPlaces
                .Include(a => a.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adPlace == null)
            {
                return NotFound();
            }

            return View(adPlace);
        }

        // POST: AdPlaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AdPlaces == null)
            {
                return Problem("Entity set 'AdAgencyContext.AdPlaces'  is null.");
            }
            var adPlace = await _context.AdPlaces.FindAsync(id);
            if (adPlace != null)
            {
                _context.AdPlaces.Remove(adPlace);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdPlaceExists(int id)
        {
          return _context.AdPlaces.Any(e => e.Id == id);
        }
    }
}
