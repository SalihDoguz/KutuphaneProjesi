using BasicBookProject.Models.Abstracts;
using BasicBookProject.Utility;

namespace BasicBookProject.Models.Concrete
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Book book)
        {
            _context.Update(book);
        }
    }
}
