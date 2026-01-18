using WebApplication14.Models;
using WebApplication14.Pagination;

namespace WebApplication14.Repository
{
    public interface IBattleRepository
    {
         Task<List<Battle>> GetAll(PaginationRequest pagination);
        Task<Battle> GetId(int id);
        Task<List<Battle>>Filtrarpornombre(string nombre, PaginationRequest pagination);
        Task<List<Battle>> OrdenarAsc(PaginationRequest pagination);
        Task<List<Battle>>OrdenarDesc(PaginationRequest pagination);
        Task Save();
        Task Update(Battle battle);
        Task Delete(Battle battle);
        Task Create(Battle battle); 

    }
}
