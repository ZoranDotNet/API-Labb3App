using API_Labb3.DTOs;
using API_Labb3.Entity;

namespace API_Labb3.Repositories
{
    public interface IPeopleRepository
    {
        Task<List<Person>> GetAll(PaginationDTO pagination);
        Task<Person?> GetById(int id);
        Task<int> Create(Person person);
        Task<bool> Exists(int id);
        Task Update(Person person);
        Task Delete(int id);
        Task<List<Person>> GetByName(string name);
        Task<Person?> GetAllLinks(int id);
    }
}
