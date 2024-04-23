using API_Labb3.Data;
using API_Labb3.Entity;
using Microsoft.EntityFrameworkCore;

namespace API_Labb3.Repositories
{
    public class LinksRepository(AppDbContext _context) : ILinksRepository
    {
        public async Task<List<Link>> GetAll(int hobbyId)
        {
            return await _context.Links.Where(l => l.HobbyId == hobbyId).ToListAsync();
        }

        public async Task<Link?> GetById(int id)
        {
            return await _context.Links.AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<int> Create(Link link)
        {
            _context.Links.Add(link);
            await _context.SaveChangesAsync();
            return link.Id;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Links.AnyAsync(l => l.Id == id);
        }

        public async Task Update(Link link)
        {
            _context.Links.Update(link);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await _context.Links.Where(l => l.Id == id).ExecuteDeleteAsync();
        }
    }
}
