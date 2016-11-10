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
    public class PostesController : Controller
    {
        private readonly FlotteContext _context;

        public PostesController(FlotteContext context)
        {
            _context = context;    
        }

        // GET: Postes
        public async Task<IActionResult> Index()
        {
            var flotteContext = _context.Postes.Include(p => p.Role).Include(p => p.TypeBateau);
            return View(await flotteContext.ToListAsync());
        }

        // Return Json list filtered by id. (url query string)
        public JsonResult getPostesType(int TypeBateauID)
        {
            var postes = _context.Postes.Include(p => p.Role).Where(p => p.TypeBateauID == TypeBateauID);
            //SelectList olist = new SelectList(_context.Postes, "PosteID", "TypeBateauID");

            return Json(postes);
        }

        // GET: Postes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poste = await _context.Postes.SingleOrDefaultAsync(m => m.PosteID == id);
            if (poste == null)
            {
                return NotFound();
            }

            return View(poste);
        }

        // GET: Postes/Create
        public IActionResult Create()
        {
            ViewData["RoleID"] = new SelectList(_context.Roles, "RoleID", "RoleID");
            ViewData["TypeBateauID"] = new SelectList(_context.TypesBateaux, "TypeBateauID", "TypeBateauID");
            return View();
        }

        // POST: Postes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PosteID,Minimum,RoleID,TypeBateauID")] Poste poste)
        {
            if (ModelState.IsValid)
            {
                _context.Add(poste);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["RoleID"] = new SelectList(_context.Roles, "RoleID", "RoleID", poste.RoleID);
            ViewData["TypeBateauID"] = new SelectList(_context.TypesBateaux, "TypeBateauID", "TypeBateauID", poste.TypeBateauID);
            return View(poste);
        }

        // GET: Postes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poste = await _context.Postes.SingleOrDefaultAsync(m => m.PosteID == id);
            if (poste == null)
            {
                return NotFound();
            }
            ViewData["RoleID"] = new SelectList(_context.Roles, "RoleID", "RoleID", poste.RoleID);
            ViewData["TypeBateauID"] = new SelectList(_context.TypesBateaux, "TypeBateauID", "TypeBateauID", poste.TypeBateauID);
            return View(poste);
        }

        // POST: Postes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PosteID,Minimum,RoleID,TypeBateauID")] Poste poste)
        {
            if (id != poste.PosteID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(poste);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PosteExists(poste.PosteID))
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
            ViewData["RoleID"] = new SelectList(_context.Roles, "RoleID", "RoleID", poste.RoleID);
            ViewData["TypeBateauID"] = new SelectList(_context.TypesBateaux, "TypeBateauID", "TypeBateauID", poste.TypeBateauID);
            return View(poste);
        }

        // GET: Postes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poste = await _context.Postes.SingleOrDefaultAsync(m => m.PosteID == id);
            if (poste == null)
            {
                return NotFound();
            }

            return View(poste);
        }

        // POST: Postes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var poste = await _context.Postes.SingleOrDefaultAsync(m => m.PosteID == id);
            _context.Postes.Remove(poste);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PosteExists(int id)
        {
            return _context.Postes.Any(e => e.PosteID == id);
        }
    }
}
