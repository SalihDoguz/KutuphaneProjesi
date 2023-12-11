using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicBookProject.Models
{
    public class Hire
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int StudentId { get; set; }
        [ValidateNever]
        public int BookId { get; set; }
        [ValidateNever]
        [ForeignKey("BookId")]
        public Book? Book { get; set; }


    }
}
