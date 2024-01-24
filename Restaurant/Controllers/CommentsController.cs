using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Areas.Identity.Data;
using Restaurant.Data;
using Restaurant.Infrastructure;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class CommentsController : Controller
    {
        private readonly UserManager<RestaurantUser> _manager;
        private readonly ApplicationDbContext2 _context;
        private readonly IComments _comments;

        public CommentsController(ApplicationDbContext2 context, UserManager<RestaurantUser> manager, IComments comments)
        {
            _manager = manager;
            _context = context;
            _comments = comments;

        }

        // GET: Comments
        public async Task<IActionResult> Index(int id)
        {
            TempData["id"] = id;
            //return View(await _context.UserComments.Where(x => x.RestroId==id).ToListAsync());
            //return View(_comments.GetAll(id));
            return Json(_comments.GetAll(id));
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comments = await _context.UserComments
                .FirstOrDefaultAsync(m => m.CmtId == id);
            if (comments == null)
            {
                return NotFound();
            }

            return View(comments);
        }

        // GET: Comments/Create
       /* public IActionResult Create()
        {
            
            return View();
        }*/

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(string content,int id)
        {
            int ids = id;
            //var id = TempData["id"];
            var usridd = _manager.GetUserId(User);
            var usrname = _manager.GetUserName(User);
            
           var comments=new Comments();
            if (ModelState.IsValid)
            {
                comments.Content = content;
                comments.UserName = usrname;
                comments.UserId= usridd;
                comments.RestroId = (int)ids;
                _comments.Insert(comments);
                _comments.save();
              return RedirectToAction("Index");
                
            }
            return View(comments);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comments = await _context.UserComments.FindAsync(id);
            if (comments == null)
            {
                return NotFound();
            }
            return View(comments);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CmtId,UserId,RestroId,Content")] Comments comments)
        {
            if (id != comments.CmtId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentsExists(comments.CmtId))
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
            return View(comments);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comments = await _context.UserComments
                .FirstOrDefaultAsync(m => m.CmtId == id);
            if (comments == null)
            {
                return NotFound();
            }

            return View(comments);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comments = await _context.UserComments.FindAsync(id);
            if (comments != null)
            {
                _context.UserComments.Remove(comments);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentsExists(int id)
        {
            return _context.UserComments.Any(e => e.CmtId == id);
        }
    }
}
