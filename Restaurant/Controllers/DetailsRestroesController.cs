using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.EntityFramework;
using Restaurant.Data;
using Restaurant.Models;


namespace Restaurant.Controllers
{
    public class DetailsRestroesController : Controller
    {
        private readonly ApplicationDbContext2 _context;

        public DetailsRestroesController(ApplicationDbContext2 context)
        {
            _context = context;
        }

        // GET: DetailsRestroes
        public async Task<IActionResult> Index()
        {
             return View(await _context.DetailsRestroo.ToListAsync());
            //int pageSize = 10; // Number of items per page
            //int pageNumber = page ?? 1; // Default to page 1 if no page is specified

            //var model = await _context.DetailsRestroo.ToPagedListAsync(pageNumber, pageSize);

            //return View(model);
        }

        // GET: DetailsRestroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detailsRestro = await _context.DetailsRestroo
                .FirstOrDefaultAsync(m => m.ID == id);
            if (detailsRestro == null)
            {
                return NotFound();
            }

            return View(detailsRestro);
        }

        // GET: DetailsRestroes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DetailsRestroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Location,Description,DetailedDescription,PhoneNo,Time,Website")] DetailsRestro detailsRestro)
        {
           
            if (ModelState.IsValid)
            {
                _context.Add(detailsRestro);
                await _context.SaveChangesAsync();
                TempData["message"] = "New Restaurant Added Successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(detailsRestro);
        }

        // GET: DetailsRestroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detailsRestro = await _context.DetailsRestroo.FindAsync(id);
            if (detailsRestro == null)
            {
                return NotFound();
            }
            return View(detailsRestro);
        }

        // POST: DetailsRestroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Location,Description,DetailedDescription,PhoneNo,Time,Website")] DetailsRestro detailsRestro)
        {
            if (id != detailsRestro.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detailsRestro);
                    TempData["message"] = "Restaurant Updated Successfully.";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetailsRestroExists(detailsRestro.ID))
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
            return View(detailsRestro);
        }

        // GET: DetailsRestroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detailsRestro = await _context.DetailsRestroo
                .FirstOrDefaultAsync(m => m.ID == id);
            if (detailsRestro == null)
            {
                return NotFound();
            }

            return View(detailsRestro);
        }

        // POST: DetailsRestroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detailsRestro = await _context.DetailsRestroo.FindAsync(id);
            if (detailsRestro != null)
            {
                _context.DetailsRestroo.Remove(detailsRestro);
            }
            TempData["message"] = "Restaurant Removed Successfully.";
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetailsRestroExists(int id)
        {
            return _context.DetailsRestroo.Any(e => e.ID == id);
        }
    }
}
