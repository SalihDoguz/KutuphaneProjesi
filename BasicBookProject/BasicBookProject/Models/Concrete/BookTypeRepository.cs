using BasicBookProject.Models.Abstracts;
using BasicBookProject.Utility;

namespace BasicBookProject.Models.Concrete
{
    public class BookTypeRepository : Repository<BookType>, IBookTypeRepository
    {
        private readonly ApplicationDbContext _context;
        public BookTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(BookType bookType)
        {
            _context.Update(bookType);     
        }
    }
}
