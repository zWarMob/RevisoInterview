using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using TimeTracker.Data;
using TimeTracker.Models;
using TimeTracker.ViewModels;

namespace TimeTracker.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<IdentityUser> _userManager;

        public InvoicesController(IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            _context = new ApplicationDbContext(optionsBuilder.Options);
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            var invoices = _context.Invoices
                .Where(x => x.UserId == _userManager.GetUserId(User))
                .Include(x => x.Customer)
                .Include(x => x.TimeEntries);

            var invoicesModel = invoices.Select(x => new InvoiceDisplayViewModel()
            {
                Date = x.Date,
                SelectedCustomerId = x.CustomerId,
                SelectedTimeEntries = x.TimeEntries.Select(y => new TimeEntryFormViewModel()
                {
                    Description = y.Description,
                    TotalPrice = y.TotalPrice
                })
            });

            return View(invoices);
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new InvoiceFormViewModel()
            {
                AvailableCustomers = _context.Customers.ToList(),

                AvailableTimeEntries = _context.TimeEntries.ToList()
                    .Select(x => new TimeEntryFormViewModel()
                    {
                        Id = x.Id,
                        Description = x.Description,
                        TotalPrice = x.TotalPrice
                    })
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(InvoiceFormViewModel invoiceModel)
        {
            Invoice invoice = new Invoice()
            {
                CustomerId = invoiceModel.SelectedCustomerId,
                Date = DateTime.Now,
                UserId = _userManager.GetUserId(User),
            };

            _context.Invoices.Add(invoice);

            _context.SaveChanges();

            var timeEntryEntities = invoiceModel.SelectedTimeEntries.Select(x => new TimeEntry() { Id = x, InvoiceId = invoice.Id });

            foreach (var timeEntryEntity in timeEntryEntities)
            {
                _context.Entry(timeEntryEntity).Property("InvoiceId").IsModified = true;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Invoices");
        }

        [Authorize]
        public IActionResult Update(int id)
        {
            var invoice = _context.Invoices.Where(x => x.Id == id).Include(x => x.TimeEntries).Single();

            var invoiceModel = new InvoiceFormViewModel()
            {
                SelectedCustomerId = invoice.CustomerId,

                SelectedTimeEntries = invoice.TimeEntries.Select(x => x.Id),

                AvailableCustomers = _context.Customers.ToList(),

                AvailableTimeEntries = _context.TimeEntries.ToList()
                    .Select(x => new TimeEntryFormViewModel()
                    {
                        Id = x.Id,
                        Description = x.Description,
                        TotalPrice = x.TotalPrice
                    })
            };

            return View(invoiceModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Update(Invoice invoice)
        {
            invoice.UserId = _userManager.GetUserId(User);

            _context.Invoices.Update(invoice);

            _context.SaveChanges();

            return RedirectToAction("Index", "Invoices");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var invoice = new Invoice() { Id = id };

            _context.Invoices.Remove(invoice);

            _context.SaveChanges();

            return RedirectToAction("Index", "Invoices");
        }
    }
}