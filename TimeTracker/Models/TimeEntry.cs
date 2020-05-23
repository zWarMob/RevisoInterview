using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TimeTracker.Models
{
    public class TimeEntry
    {
        public int Id { get; set; }

        [Required]
        public IdentityUser User { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Hours { get; set; }

        [Required]
        public int HourlyPrice { get; set; }
    }
}
