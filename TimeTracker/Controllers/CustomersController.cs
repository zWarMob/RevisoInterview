﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

            return RedirectToAction("Index", "Home");
        }
    }
}