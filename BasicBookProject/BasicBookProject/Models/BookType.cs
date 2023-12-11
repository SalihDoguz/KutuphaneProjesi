using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BasicBookProject.Models
{
    public class BookType
    {
        // Primary Key ([Key] yazılmasada farklı isim(BookTypeId) olmamak şartıyla program primary key olarak kabul ediyor..)
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Kitap Tür Adı Boş Bırakılamaz! ")] // Not null anlamına gelir.
        [DisplayName("Kitap Türü Adı"),MaxLength(50)]
        public string? Name { get; set; }

        // Öncesinden yazıyorum
        //public ICollection<Book> Books { get; set; }
        //public List<Book>? Books { get; set; }
    }
}
