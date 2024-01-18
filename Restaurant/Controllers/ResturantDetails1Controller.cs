﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class ResturantDetails1Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResturantDetails1Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ResturantDetails1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Details.ToListAsync());
        }

        // GET: ResturantDetails1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resturantDetail = await _context.Details
                .FirstOrDefaultAsync(m => m.ID == id);
            if (resturantDetail == null)
            {
                return NotFound();
            }

            return View(resturantDetail);
        }

        // GET: ResturantDetails1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ResturantDetails1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Location,Description")] ResturantDetail resturantDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resturantDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(resturantDetail);
        }

        // GET: ResturantDetails1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resturantDetail = await _context.Details.FindAsync(id);
            if (resturantDetail == null)
            {
                return NotFound();
            }
            return View(resturantDetail);
        }

        // POST: ResturantDetails1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Location,Description")] ResturantDetail resturantDetail)
        {
            if (id != resturantDetail.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resturantDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResturantDetailExists(resturantDetail.ID))
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
            return View(resturantDetail);
        }

        // GET: ResturantDetails1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resturantDetail = await _context.Details
                .FirstOrDefaultAsync(m => m.ID == id);
            if (resturantDetail == null)
            {
                return NotFound();
            }

            return View(resturantDetail);
        }

        // POST: ResturantDetails1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resturantDetail = await _context.Details.FindAsync(id);
            if (resturantDetail != null)
            {
                _context.Details.Remove(resturantDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResturantDetailExists(int id)
        {
            return _context.Details.Any(e => e.ID == id);
        }
    }
}
