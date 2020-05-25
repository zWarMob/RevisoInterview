using System;
using System.Collections.Generic;

namespace TimeTracker.ViewModels
{
    public class InvoiceDisplayViewModel
    {
        public int SelectedCustomerId { get; set; }

        public IEnumerable<TimeEntryFormViewModel> SelectedTimeEntries { get; set; }

        public DateTime Date { get; set; }
    }
}
