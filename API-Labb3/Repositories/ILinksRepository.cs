using API_Labb3.Entity;

namespace API_Labb3.Repositories
{
    public interface ILinksRepository
    {
        Task<int> Create(Link link);
        Task Delete(int id);
        Task<bool> Exists(int id);
        Task<List<Link>> GetAll(int hobbyId);
        Task<Link?> GetById(int id);
        Task Update(Link link);
    }
}