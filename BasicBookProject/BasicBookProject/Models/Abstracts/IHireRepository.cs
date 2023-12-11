namespace BasicBookProject.Models.Abstracts
{
    public interface IHireRepository :IRepository<Hire>
    {
        void Update(Hire hire);
        void Save();
    }
}
