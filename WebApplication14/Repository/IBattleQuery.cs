using WebApplication14.DTOS.Response;
using WebApplication14.Models;
using WebApplication14.Pagination;

namespace WebApplication14.Repository
{
    public interface IBattleQuery
    {
         Task<List<Battlereponse>> GetAll(PaginationRequest pagination);
        Task<Battlereponse?> GetById(int id);
        Task<List<Battlereponse>> FiltrarPornombre(string nombre, PaginationRequest pagination);
        Task<List<Battlereponse>> OrdenarAsc(PaginationRequest pagination);
        Task<List<Battlereponse>> OrdenarDesc(PaginationRequest pagination);


    }
}
