using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using GestionFlotte.Data;
using GestionFlotte.Models.SchoolViewModels;

namespace GestionFlotte.Controllers
{
    public class HomeController : Controller
    {
        private readonly FlotteContext _context;
        
        public HomeController(FlotteContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        /*
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        */
        public async Task<ActionResult> About()
        {
            IQueryable<EnrollmentDateGroup> data =
                from student in _context.Students
                group student by student.EnrollmentDate into dateGroup
                select new EnrollmentDateGroup()
                {
                    EnrollmentDate = dateGroup.Key,
                    StudentCount = dateGroup.Count()
                };
            return View(await data.AsNoTracking().ToListAsync());
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
