using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using Restaurant.Infrastructure;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class RatingsController : Controller
    {
        private readonly ApplicationDbContext2 _context;
        private readonly IRatings _rating;


        public RatingsController(ApplicationDbContext2 context,IRatings rating)
        {
            _context = context;
            _rating = rating;
        }

        // GET: Ratings
        public async Task<IActionResult> Index(int id)
        {
            return Json(_rating.GetAll(id));
            //return Json(_context.UserRatings.Where(x => x.RestroID==id));
            // return View(await _context.UserRatings.ToListAsync());
        }

  
        [HttpPost]
       
        public async Task<IActionResult> Create(int content,int id)
        {
            int ids = id;
            int usrrating = content;
            var objs = new Rating();
            if (ModelState.IsValid)
            {
                objs.Ratings = usrrating;//string to int
                objs.RestroID = ids;
               _rating.Insert(objs);
                 //_context.Add(objs);
               // _context.SaveChanges();
                _rating.save();
                TempData["rate"] = "Rating added successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

 

        private bool RatingExists(int id)
        {
            return _context.UserRatings.Any(e => e.RateID == id);
        }
    }
}
