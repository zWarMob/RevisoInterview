using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using TimeTracker.Data;
using TimeTracker.Models;

namespace TimeTracker.Controllers
{
    public class TimeEntriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<IdentityUser> _userManager;

        public TimeEntriesController(IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            _context = new ApplicationDbContext(optionsBuilder.Options);
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View(_context.TimeEntries.Where(x => x.UserId == _userManager.GetUserId(User)));
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(TimeEntry timeEntry)
        {
            timeEntry.UserId = _userManager.GetUserId(User);

            _context.TimeEntries.Add(timeEntry);
            _context.SaveChanges();

            return RedirectToAction("Index", "TimeEntries");
        }

        [Authorize]
        public IActionResult Update(int id)
        {
            return View(_context.TimeEntries.Find(id));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Update(TimeEntry timeEntry)
        {
            _context.TimeEntries.Update(timeEntry);
            _context.SaveChanges();

            return RedirectToAction("Index", "TimeEntries");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var timeEntry = _context.TimeEntries.Find(id);

            _context.TimeEntries.Remove(timeEntry);

            _context.SaveChanges();

            return RedirectToAction("Index", "TimeEntries");
        }
    }
}