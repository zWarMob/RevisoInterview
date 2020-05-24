using System;
using System.Collections.Generic;
using TimeTracker.Models;

namespace TimeTracker.ViewModels
{
    public class InvoiceFormViewModel
    {
        public string UserId { get; set; }

        public int SelectedCustomerId { get; set; }

        public IEnumerable<Customer> AvailableCustomers { get; set; }

        public IEnumerable<int> SelectedTimeEntries { get; set; }

        public IEnumerable<TimeEntry> AvailableTimeEntries { get; set; }

        public DateTime Date { get; set; }
    }

    public class SelectListItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
}
