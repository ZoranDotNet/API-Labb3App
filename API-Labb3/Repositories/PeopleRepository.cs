using API_Labb3.Data;
using API_Labb3.DTOs;
using API_Labb3.Entity;
using Microsoft.EntityFrameworkCore;

namespace API_Labb3.Repositories
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PeopleRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Person>> GetAll(PaginationDTO pagination)
        {
            var queryable = _context.People.AsQueryable();
            await _httpContextAccessor.HttpContext!.InsertPaginationParametersInResponseHeader(queryable);
            return await queryable.OrderBy(p => p.Name).Paginate(pagination).ToListAsync();
        }

        public async Task<Person?> GetById(int id)
        {
            return await _context.People.Include(p => p.Hobbies).ThenInclude(h => h.Links).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Person>> GetByName(string name)
        {
            return await _context.People.Include(p => p.Hobbies).ThenInclude(h => h.Links).Where(p => p.Name.Contains(name)).ToListAsync();
        }

        public async Task<int> Create(Person person)
        {
            _context.People.Add(person);
            await _context.SaveChangesAsync();
            return person.Id;
        }

        public async Task Update(Person person)
        {
            _context.Update(person);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await _context.People.Where(p => p.Id == id).ExecuteDeleteAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.People.AnyAsync(p => p.Id == id);
        }

        public async Task<Person?> GetAllLinks(int id)
        {
            return await _context.People.Include(p => p.Hobbies).ThenInclude(h => h.Links).FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
