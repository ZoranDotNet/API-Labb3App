using API_Labb3.Data;
using API_Labb3.Entity;
using Microsoft.EntityFrameworkCore;

namespace API_Labb3.Repositories
{
    public class HobbiesRepository(AppDbContext _context) : IHobbiesRepository
    {
        public async Task<List<Hobby>> GetAll(int personId)
        {
            return await _context.Hobbies.Include(h => h.Links).Where(h => h.PersonId == personId).ToListAsync();
        }

        public async Task<Hobby?> GetById(int id)
        {
            return await _context.Hobbies.Include(h => h.Links).AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<int> Create(Hobby hobby)
        {
            _context.Add(hobby);
            await _context.SaveChangesAsync();
            return hobby.Id;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Hobbies.AnyAsync(h => h.Id == id);
        }

        public async Task Update(Hobby hobby)
        {
            _context.Update(hobby);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await _context.Hobbies.Where(h => h.Id == id).ExecuteDeleteAsync();
        }

        public async Task<List<Hobby>> GetByTitle(string title)
        {
            return await _context.Hobbies.Include(h => h.Links).Where(h => h.Title.Contains(title)).ToListAsync();
        }
    }
}
