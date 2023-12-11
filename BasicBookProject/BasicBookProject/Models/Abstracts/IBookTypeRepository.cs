namespace BasicBookProject.Models.Abstracts
{
    public interface IBookTypeRepository : IRepository<BookType>
    {
        void Update(BookType bookType);
        void Save();
    }
}
