using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TimeTracker.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        [Required]
        public IdentityUser User { get; set; }

        [Required]
        public Customer Customer { get; set; }

        public List<TimeEntry> TimeEntries { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
