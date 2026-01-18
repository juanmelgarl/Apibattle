using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApplication14.Models;
using WebApplication14.Pagination;

namespace WebApplication14.Repository
{
    public class BattleRepository : IBattleRepository
    {
        private readonly GotDbContext _dbcontext;
        public BattleRepository(GotDbContext dbcontext)
        {
              _dbcontext = dbcontext;
        }
        public async Task<List<Battle>> GetAll(PaginationRequest pagination)
        {
            return await _dbcontext.Battles
                .AsNoTracking()
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();
        }
        public async Task<Battle?> GetId(int id)
        {
          return await _dbcontext.Battles.FirstOrDefaultAsync(C => C.Id == id);
        }
        public async Task<List<Battle>> Filtrarpornombre(string nombre, PaginationRequest pagination)
        {
            return await _dbcontext.Battles.Where(c => c.Name.Contains(nombre))
                .AsNoTracking()
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();
        }
        public async Task Save()
        {
            await _dbcontext.SaveChangesAsync();    
        }
        public async Task<List<Battle>> OrdenarAsc(PaginationRequest pagination)
        {
            return await _dbcontext.Battles.OrderBy(c => c.Id)
                .AsNoTracking()
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();
        }
        public async Task<List<Battle>> OrdenarDesc(PaginationRequest pagination)
        {
            return await _dbcontext.Battles.OrderByDescending(c => c.Id)
                .AsNoTracking()
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();
        }
        public async Task Delete(Battle battle)
        {
            _dbcontext.Battles.Remove(battle);
        }
        public async Task Update(Battle battle)
        {
            _dbcontext.Battles.Update(battle);
        }
        public async Task Create(Battle battle)
        {
            _dbcontext.Battles.Add(battle);
        }
    }
}
