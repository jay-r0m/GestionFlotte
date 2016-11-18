using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionFlotte.Data;
using GestionFlotte.Models;

namespace GestionFlotte.Controllers
{
    public class BateauxController : Controller
    {
        private readonly FlotteContext _context;

        public BateauxController(FlotteContext context)
        {
            _context = context;    
        }

        // GET: Bateaux
        public async Task<IActionResult> Index()
        {
            var flotteContext = _context.Bateaux
                .Include(b => b.TypeBateau)
                    .ThenInclude(t => t.Postes)
                        .ThenInclude(p => p.Role)
                .Include(b => b.Marins)
                    .ThenInclude(m => m.Roles)
                        .ThenInclude(r => r.Role)
                ;
            return View(await flotteContext.ToListAsync());
        }

        // GET: Bateaux/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //            var bateau = await _context.Bateaux.SingleOrDefaultAsync(m => m.ID == id);

            var bateau = await _context.Bateaux
                .Include(b => b.TypeBateau)
                    .ThenInclude(t => t.Postes)
                        .ThenInclude(r => r.Role)
                .Include(b => b.Marins)
                    .ThenInclude(m => m.Roles)
                        .ThenInclude(r => r.Role)
                .AsNoTracking()
                .SingleOrDefaultAsync(b => b.ID == id);

            if (bateau == null)
            {
                return NotFound();
            }

            return View(bateau);
        }

        // GET: Bateaux/Create
        public IActionResult Create()
        {
            ViewBag.Roles = _context.Roles;
            ViewData["TypeBateauID"] = new SelectList(_context.TypesBateaux, "TypeBateauID", "Designation");
            return View();
        }

        // POST: Bateaux/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bateau bateau)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(bateau);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            ViewData["TypeBateauID"] = new SelectList(_context.TypesBateaux, "TypeBateauID", "TypeBateauID", bateau.TypeBateauID);
            return View(bateau);
        }

        // GET: Bateaux/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bateau = await _context.Bateaux.SingleOrDefaultAsync(m => m.ID == id);
            if (bateau == null)
            {
                return NotFound();
            }
            ViewData["TypeBateauID"] = new SelectList(_context.TypesBateaux, "TypeBateauID", "Designation", bateau.TypeBateauID);
            return View(bateau);
        }

        // POST: Bateaux/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Bateau bateau)
        {
            if (id != bateau.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bateau);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BateauExists(bateau.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["TypeBateauID"] = new SelectList(_context.TypesBateaux, "TypeBateauID", "TypeBateauID", bateau.TypeBateauID);
            return View(bateau);
        }

        // GET: Bateaux/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bateau = await _context.Bateaux.SingleOrDefaultAsync(m => m.ID == id);
            if (bateau == null)
            {
                return NotFound();
            }

            return View(bateau);
        }

        // POST: Bateaux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bateau = await _context.Bateaux.SingleOrDefaultAsync(m => m.ID == id);
            _context.Bateaux.Remove(bateau);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BateauExists(int id)
        {
            return _context.Bateaux.Any(e => e.ID == id);
        }
    }
}
