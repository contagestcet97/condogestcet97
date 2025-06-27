using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using condogestcet97.web.Data;
using condogestcet97.web.Data.Entities.Condominium;

namespace condogestcet97.web.Controllers
{
    public class CondosController : Controller
    {
        private readonly DataContextCondominium _context;

        public CondosController(DataContextCondominium context)
        {
            _context = context;
        }

        // GET: Condos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Condos.ToListAsync());
        }

        // GET: Condos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var condo = await _context.Condos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (condo == null)
            {
                return NotFound();
            }

            return View(condo);
        }

        // GET: Condos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Condos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Address")] Condo condo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(condo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(condo);
        }

        // GET: Condos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var condo = await _context.Condos.FindAsync(id);
            if (condo == null)
            {
                return NotFound();
            }
            return View(condo);
        }

        // POST: Condos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Address")] Condo condo)
        {
            if (id != condo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(condo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CondoExists(condo.Id))
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
            return View(condo);
        }

        // GET: Condos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var condo = await _context.Condos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (condo == null)
            {
                return NotFound();
            }

            return View(condo);
        }

        // POST: Condos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var condo = await _context.Condos.FindAsync(id);
            if (condo != null)
            {
                _context.Condos.Remove(condo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CondoExists(int id)
        {
            return _context.Condos.Any(e => e.Id == id);
        }
    }
}
