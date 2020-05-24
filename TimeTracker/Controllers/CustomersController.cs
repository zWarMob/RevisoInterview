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
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<IdentityUser> _userManager;

        public CustomersController(IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            _context = new ApplicationDbContext(optionsBuilder.Options);
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View(_context.Customers.Where(x => x.UserId == _userManager.GetUserId(User)));
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            customer.UserId = _userManager.GetUserId(User);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        [Authorize]
        public IActionResult Update(int id)
        {
            return View(_context.Customers.Find(id));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Update(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var customer = _context.Customers.Find(id);

            _context.Customers.Remove(customer);

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }
    }
}