using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionFlotte.Data;
using GestionFlotte.Models;
using System.Collections;
using GestionFlotte.Models.FleetViewModels;

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
        public async Task<IActionResult> Index(int? id, int? roleID)
        {
            var viewModel = new MarinIndexData();
            viewModel.Marins = await _context.Marins
                .Include(m => m.Roles)
                    .ThenInclude(r => r.Role)
                .Include(i => i.Roles)
                    .ThenInclude(i => i.Role)
                .Include(m => m.Bateau)

                  .OrderBy(i => i.Nom)
                  .ToListAsync();

            if (id != null)
            {
                ViewData["MarinID"] = id.Value;
                Marin marin = viewModel.Marins.Where(
                    i => i.ID == id.Value).Single();
                viewModel.Roles = marin.Roles.Select(s => s.Role);
            }

            return View(viewModel);
        }

        // GET: Marins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marin = await _context.Marins
                //.Include(s => s.Maitrises)
                //    .ThenInclude(e => e.Role)
                .Include(r => r.Roles)
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
            PopulateBateauxDropDownList();

            var marin = new Marin();
            marin.Roles = new List<RoleAssignment>();
            PopulateAssignedRoleData(marin);

            return View();
        }

        // POST: Marins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID, Nom, Prenom, BateauID")] Marin marin, string[] selectedRoles)
        {
            if (selectedRoles != null)
            {
                marin.Roles = new List<RoleAssignment>();
                foreach (var role in selectedRoles)
                {
                    var roleToAdd = new RoleAssignment { MarinID = marin.ID, RoleID = int.Parse(role) };
                    marin.Roles.Add(roleToAdd);
                }
            }
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

            var marin = await _context.Marins
                .Include(m => m.Roles)
                    .ThenInclude(r => r.Role)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);

            if (marin == null)
            {
                return NotFound();
            }
            PopulateAssignedRoleData(marin);

            PopulateBateauxDropDownList(marin.BateauID);

            return View(marin);
        }

        private void PopulateAssignedRoleData(Marin marin)
        {
            var allRoles = _context.Roles;
            var marinRoles = new HashSet<int>(marin.Roles.Select(c => c.RoleID));
            var viewModel = new List<AssignedRoleData>();
            foreach (var role in allRoles)
            {
                viewModel.Add(new AssignedRoleData
                {
                    RoleID = role.RoleID,
                    RoleName = role.Nom,
                    Assigned = marinRoles.Contains(role.RoleID)
                });
            }
            ViewData["Roles"] = viewModel;
        }

        // POST: Marins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedCourses)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marinToUpdate = await _context.Marins
                .Include(m => m.Roles)
                    .ThenInclude(r => r.Role)
                .SingleOrDefaultAsync(m => m.ID == id);

            if (await TryUpdateModelAsync<Marin>(
                marinToUpdate,
                "",
                i => i.Prenom, i => i.Nom, i => i.BateauID))
            {

                UpdateMarinRoles(selectedCourses, marinToUpdate);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction("Index");
            }
            return View(marinToUpdate);
        }

        private void UpdateMarinRoles(string[] selectedRoles, Marin marinToUpdate)
        {
            if (selectedRoles == null)
            {
                marinToUpdate.Roles = new List<RoleAssignment>();
                return;
            }

            var selectedRolesHS = new HashSet<string>(selectedRoles);
            var instructorRoles = new HashSet<int>(marinToUpdate.Roles.Select(r => r.Role.RoleID));
            foreach (var role in _context.Roles)
            {
                if (selectedRolesHS.Contains(role.RoleID.ToString()))
                {
                    if (!instructorRoles.Contains(role.RoleID))
                    {
                        marinToUpdate.Roles.Add(new RoleAssignment { MarinID = marinToUpdate.ID, RoleID = role.RoleID });
                    }
                }
                else
                {

                    if (instructorRoles.Contains(role.RoleID))
                    {
                        RoleAssignment roleToRemove = marinToUpdate.Roles.SingleOrDefault(i => i.RoleID == role.RoleID);
                        _context.Remove(roleToRemove);
                    }
                }
            }
        }

        private void PopulateBateauxDropDownList(object selectedBateau = null)
        {
            var bateauxQuery = from d in _context.Bateaux
                                   orderby d.Nom
                                   select d;
            ViewBag.Bateaux = new SelectList(bateauxQuery.AsNoTracking(), "ID", "Nom", selectedBateau);
        }

        private void PopulateRolesDropDownList(ArrayList selectedBateau)
        {
            var rolesQuery = from d in _context.Roles
                               orderby d.Nom
                               select d;
            ViewBag.Roles = new MultiSelectList(rolesQuery.AsNoTracking(), "ID", "Nom", selectedBateau);
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
