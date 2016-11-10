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
    public class MarinsController : Controller
    {
        private readonly FlotteContext _context;

        public MarinsController(FlotteContext context)
        {
            _context = context;    
        }

        // GET: Marins
        public async Task<IActionResult> Index()
        {
            return View(await _context.Marins.ToListAsync());
        }

        // GET: Marins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marin = await _context.Marins
                .Include(s => s.Maitrises)
                    .ThenInclude(e => e.Role)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);

            if (marin == null)
            {
                return NotFound();
            }

            return View(marin);
        }

        // GET: Marins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Marins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nom,Prenom")] Marin marin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(marin);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(marin);
        }

        // GET: Marins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marin = await _context.Marins.SingleOrDefaultAsync(m => m.ID == id);
            if (marin == null)
            {
                return NotFound();
            }
            return View(marin);
        }

        // POST: Marins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nom,Prenom")] Marin marin)
        {
            if (id != marin.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(marin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarinExists(marin.ID))
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
            return View(marin);
        }

        // GET: Marins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marin = await _context.Marins.SingleOrDefaultAsync(m => m.ID == id);
            if (marin == null)
            {
                return NotFound();
            }

            return View(marin);
        }

        // POST: Marins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var marin = await _context.Marins.SingleOrDefaultAsync(m => m.ID == id);
            _context.Marins.Remove(marin);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MarinExists(int id)
        {
            return _context.Marins.Any(e => e.ID == id);
        }
    }
}
