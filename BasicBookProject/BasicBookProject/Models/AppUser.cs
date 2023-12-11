using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BasicBookProject.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        public int StudentNo { get; set; }
        public string? Address { get; set; }
        public string? Faculty { get; set; }
        public string? Section { get; set; }
    }
}
