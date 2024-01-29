using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.EntityFramework;
using Restaurant.Data;
using Restaurant.Models;
using PagedList.Mvc;
using PagedList;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using NuGet.Packaging.Signing;
using System.IO;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Restaurant.Infrastructure;
using Microsoft.AspNetCore.Authorization;


namespace Restaurant.Controllers
{
    public class DetailsRestroesController : Controller
    {
        private readonly ApplicationDbContext2 _context;
        private readonly IRestro _restro;
        private readonly IComments _ucomment;
        private readonly IRatings _rate;

       

        private readonly IWebHostEnvironment _hostEnvironment;

        public DetailsRestroesController(ApplicationDbContext2 context, IWebHostEnvironment hostEnvironment, IRestro restro,IComments ucomment,IRatings rate)
        {
            _restro = restro;
            _context = context;
            _ucomment = ucomment;
            _hostEnvironment = hostEnvironment;
            _rate = rate;
        }

        // GET: DetailsRestroes
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var restroo = _restro.GetAll();
           return View(restroo);
            //return View(await _context.DetailsRestroo.ToListAsync());

        }
        
       
        [Authorize]
        // GET: DetailsRestroes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            TempData["id"]=id;
            if (id == null)
            {
                return NotFound();
            }
            var detailsRestro = _restro.GetByID(id);
            //var detailsRestro = await _context.DetailsRestroo
                //.FirstOrDefaultAsync(m => m.ID == id);
            if (detailsRestro == null)
            {
                return NotFound();
            }

            return View(detailsRestro);
        }
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> Create([Bind("ID,Name,Location,Description,DetailedDescription,PhoneNo,Time,CloseTime,Website")] DetailsRestro detailsRestro,IFormFile photoFile)
        {

            if (ModelState.IsValid)
            {
                if (photoFile != null && photoFile.Length > 0)
                {
                    // Generate a unique file name based on GUID
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(photoFile.FileName);

                    // Set the path where you want to save the photo
                    string path = Path.Combine(_hostEnvironment.WebRootPath, "lib", "Images", fileName);

                    // Save the file to the server
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await photoFile.CopyToAsync(fileStream);
                    }

                    detailsRestro.Photo = fileName;
                }

                _restro.Insert(detailsRestro);
                _restro.save();
                TempData["message"] = "New Restaurant Added Successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(detailsRestro);
        }

        // GET: DetailsRestroes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detailsRestro = _restro.GetByID(id);
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
        [ActionName("Edit")]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Location,Description,DetailedDescription,PhoneNo,Time,CloseTime,Website")] DetailsRestro detailsRestro, IFormFile? photoFile)
        {
            if (id != detailsRestro.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (photoFile != null && photoFile.Length > 0)
                        {
                            // Generate a unique file name based on GUID
                            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(photoFile.FileName);

                            // Set the path where you want to save the photo
                            string path = Path.Combine(_hostEnvironment.WebRootPath, "lib", "Images", fileName);

                            // Save the file to the server
                            using (var fileStream = new FileStream(path, FileMode.Create))
                            {
                                await photoFile.CopyToAsync(fileStream);
                            }

                            detailsRestro.Photo = fileName;
                        }
                        else
                        {
                            DetailsRestro existingDetailsRestro = await _context.DetailsRestroo.FirstOrDefaultAsync(r => r.ID == id);

                            // Detach the existingDetailsRestro from the context
                            _context.Entry(existingDetailsRestro).State = EntityState.Detached;

                            // Assign the existing photo value to detailsRestro.Photo
                            detailsRestro.Photo = existingDetailsRestro.Photo;

                        }
                        _restro.Update(detailsRestro);
                     
                        TempData["message"] = "Restaurant Updated Successfully.";
                        await _context.SaveChangesAsync();
                    }
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
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var detailsRestro = _restro.GetByID(id);
            //var detailsRestro = await _context.DetailsRestroo
               // .FirstOrDefaultAsync(m => m.ID == id);
            if (detailsRestro == null)
            {
                return NotFound();
            }

            return View(detailsRestro);
        }

        // POST: DetailsRestroes/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detailsRestro= _restro.GetByID(id);
            
            //var detailsRestro = await _context.DetailsRestroo.FindAsync(id);
            if (detailsRestro != null)
            {
                _ucomment.DelById(id);
                _ucomment.save();
                _rate.Delete(id);
                _rate.save();
                _restro.Delete(detailsRestro);
            }
            TempData["message"] = "Restaurant Removed Successfully.";
             _restro.save();
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        private bool DetailsRestroExists(int id)
        {
            return _context.DetailsRestroo.Any(e => e.ID == id);
        }

        
       
    }
}
