using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicBookProject.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required,Display(Name ="Kitap Adı")]
        public string? BookName { get; set; }
        [Display(Name ="Açıklama")]
        public string Description { get; set; }
        [Required,Display(Name ="Yazar")]
        public string Author { get; set; }
        [Required,Range(10,5000),Display(Name ="Fiyat")]
        public double Price { get; set; }
        //[Display(Name ="Kitap Türü")]
        [ValidateNever]
        public int BookTypeId { get; set; }
        [ForeignKey("BookTypeId")]
        [ValidateNever]
        public BookType? BookType { get; set; }
        [ValidateNever]
        public string? ImageURL { get; set; }

    }
}
