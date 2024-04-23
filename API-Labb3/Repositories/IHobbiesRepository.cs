using API_Labb3.Entity;

namespace API_Labb3.Repositories
{
    public interface IHobbiesRepository
    {

        Task<int> Create(Hobby hobby);
        Task Delete(int id);
        Task<bool> Exists(int id);
        Task<List<Hobby>> GetAll(int personId);
        Task<Hobby?> GetById(int id);
        Task<List<Hobby>> GetByTitle(string title);
        Task Update(Hobby hobby);
    }
}