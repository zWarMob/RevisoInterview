using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TimeTracker.Models
{
    public class TimeEntry
    {
        public int Id { get; set; }

        public IdentityUser User { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Hours { get; set; }

        [Required]
        public int HourlyPrice { get; set; }

        public Invoice Invoice { get; set; }

        public int? InvoiceId { get; set; }
    }
}
