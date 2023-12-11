using BasicBookProject.Models.Abstracts;
using BasicBookProject.Utility;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BasicBookProject.Models.Concrete
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            this._dbSet = _context.Set<T>();
            _context.Books.Include(k => k.BookType).Include(k => k.BookTypeId);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public T Get(Expression<Func<T, bool>> expression, string? includeProps = null)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(expression);

            if(!string.IsNullOrEmpty(includeProps))
            {
                foreach (var includeProp in includeProps.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProps = null)
        {
            IQueryable<T> query = _dbSet;

            if(!string.IsNullOrEmpty(includeProps))
            {
                foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList(); // await koyup ToListAsync' de kullanılabilir.
        }
    }
}
