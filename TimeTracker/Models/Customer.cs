using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TimeTracker.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public IdentityUser User { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(1024)]
        public string Address { get; set; }
    }
}
