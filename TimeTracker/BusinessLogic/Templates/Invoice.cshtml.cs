using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using TimeTracker.Models;
using TimeTracker.ViewModels;

namespace TimeTracker.BusinessLogic.Templates
{
    public class InvoiceReportModel : PageModel
    {
        public int Id { get; set; }

        public IdentityUser User { get; set; }

        public Customer Customer { get; set; }

        public List<TimeEntryFormViewModel> TimeEntries { get; set; }

        public DateTime Date { get; set; }

        public void OnGet()
        {
        }
    }
}
