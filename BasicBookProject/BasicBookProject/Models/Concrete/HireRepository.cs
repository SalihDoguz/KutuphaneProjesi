using BasicBookProject.Controllers;
using BasicBookProject.Models.Abstracts;
using BasicBookProject.Utility;

namespace BasicBookProject.Models.Concrete
{
    public class HireRepository : Repository<Hire>, IHireRepository
    {
        private readonly ApplicationDbContext _context;

        public HireRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Hire hire)
        {
            _context.Update(hire);
        }
    }
}
