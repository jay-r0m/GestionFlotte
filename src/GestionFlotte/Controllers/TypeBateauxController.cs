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
    public class TypeBateauxController : Controller
    {
        private readonly FlotteContext _context;

        public TypeBateauxController(FlotteContext context)
        {
            _context = context;    
        }

        // GET: TypeBateaux
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypesBateaux.ToListAsync());
        }

        // GET: TypeBateaux/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeBateau = await _context.TypesBateaux.SingleOrDefaultAsync(m => m.TypeBateauID == id);
            if (typeBateau == null)
            {
                return NotFound();
            }

            return View(typeBateau);
        }

        // GET: TypeBateaux/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeBateaux/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeBateauID,Designation")] TypeBateau typeBateau)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeBateau);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(typeBateau);
        }

        // GET: TypeBateaux/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeBateau = await _context.TypesBateaux.SingleOrDefaultAsync(m => m.TypeBateauID == id);
            if (typeBateau == null)
            {
                return NotFound();
            }
            return View(typeBateau);
        }

        // POST: TypeBateaux/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TypeBateauID,Designation")] TypeBateau typeBateau)
        {
            if (id != typeBateau.TypeBateauID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeBateau);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeBateauExists(typeBateau.TypeBateauID))
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
            return View(typeBateau);
        }

        // GET: TypeBateaux/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeBateau = await _context.TypesBateaux.SingleOrDefaultAsync(m => m.TypeBateauID == id);
            if (typeBateau == null)
            {
                return NotFound();
            }

            return View(typeBateau);
        }

        // POST: TypeBateaux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeBateau = await _context.TypesBateaux.SingleOrDefaultAsync(m => m.TypeBateauID == id);
            _context.TypesBateaux.Remove(typeBateau);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TypeBateauExists(int id)
        {
            return _context.TypesBateaux.Any(e => e.TypeBateauID == id);
        }
    }
}
