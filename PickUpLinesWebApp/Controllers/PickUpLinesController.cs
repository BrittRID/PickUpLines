using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PickUpLinesWebApp.Data;
using PickUpLinesWebApp.Models;

namespace PickUpLinesWebApp.Controllers
{
    public class PickUpLinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PickUpLinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PickUpLines
        public async Task<IActionResult> Index()
        {
            return View(await _context.PickUpLine.ToListAsync());
        }

        // GET: PickUpLines/Search for pick up lines
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // GET: PickUpLines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pickUpLine = await _context.PickUpLine
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pickUpLine == null)
            {
                return NotFound();
            }

            return View(pickUpLine);
        }

        // GET: PickUpLines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PickUpLines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PickQuestion,PickAnswer")] PickUpLine pickUpLine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pickUpLine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pickUpLine);
        }

        // GET: PickUpLines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pickUpLine = await _context.PickUpLine.FindAsync(id);
            if (pickUpLine == null)
            {
                return NotFound();
            }
            return View(pickUpLine);
        }

        // POST: PickUpLines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PickQuestion,PickAnswer")] PickUpLine pickUpLine)
        {
            if (id != pickUpLine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pickUpLine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PickUpLineExists(pickUpLine.Id))
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
            return View(pickUpLine);
        }

        // GET: PickUpLines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pickUpLine = await _context.PickUpLine
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pickUpLine == null)
            {
                return NotFound();
            }

            return View(pickUpLine);
        }

        // POST: PickUpLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pickUpLine = await _context.PickUpLine.FindAsync(id);
            _context.PickUpLine.Remove(pickUpLine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PickUpLineExists(int id)
        {
            return _context.PickUpLine.Any(e => e.Id == id);
        }
    }
}
