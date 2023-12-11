namespace BasicBookProject.Models.Abstracts
{
    public interface IBookRepository : IRepository<Book>
    {
        void Update(Book book);
        void Save();
    }
}
