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
    public class AdTypesController : Controller
    {
        private readonly AdAgencyContext _context;
        private readonly int _pageSize = 12;

        private string _http_string_adType = "AdType";
        public AdTypesController(AdAgencyContext context)
        {
            _context = context;
        }

        public IActionResult Index(string? adTypeName, int page = 1, SortState sortOrder = SortState.NumberAsc)
        {
            IEnumerable<AdType> adTypes = _context.AdTypes.ToList();
            
            if (!string.IsNullOrEmpty(adTypeName))
            {
                adTypes = adTypes.Where(f => f.Name!.Contains(adTypeName));
                Response.Cookies.Append(_http_string_adType, adTypeName);
            }
            else
            {
                if (Request.Cookies.ContainsKey(_http_string_adType))
                {
                    adTypeName = Request.Cookies[_http_string_adType];
                    Response.Cookies.Delete(_http_string_adType);
                }
            }

            switch(sortOrder)
            {
                case SortState.NameDesc:
                    adTypes = adTypes.OrderByDescending(f => f.Name);
                    break;
                case SortState.NameAsc:
                    adTypes = adTypes.OrderBy(f => f.Name);
                    break;
                case SortState.NumberDesc:
                
                    adTypes = adTypes.OrderByDescending(f => f.Id);
                    break;
                default:
                    adTypes = adTypes.OrderBy(f => f.Id);
                    break;
            }

            var count = adTypes.Count();
            var items = adTypes.Skip((page - 1) * _pageSize).Take(_pageSize).ToList();
            
            AdTypesViewModel viewModel = new AdTypesViewModel(items,
                new PageViewModel(count,page,_pageSize),
                new AdTypeSortViewModel(sortOrder),
                new AdTypeFilterViewModel(adTypeName)
            );
            return View(viewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AdTypes == null)
            {
                return NotFound();
            }

            var adType = await _context.AdTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adType == null)
            {
                return NotFound();
            }

            return View(adType);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] AdType adType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adType);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AdTypes == null)
            {
                return NotFound();
            }

            var adType = await _context.AdTypes.FindAsync(id);
            if (adType == null)
            {
                return NotFound();
            }
            return View(adType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] AdType adType)
        {
            if (id != adType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdTypeExists(adType.Id))
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
            return View(adType);
        }

        // GET: AdTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AdTypes == null)
            {
                return NotFound();
            }

            var adType = await _context.AdTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adType == null)
            {
                return NotFound();
            }

            return View(adType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AdTypes == null)
            {
                return Problem("Entity set 'AdAgencyContext.AdTypes'  is null.");
            }
            var adType = await _context.AdTypes.FindAsync(id);
            if (adType != null)
            {
                _context.AdTypes.Remove(adType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdTypeExists(int id)
        {
          return _context.AdTypes.Any(e => e.Id == id);
        }
    }
}
