using WebApplication14.DTOS.Reques;
using WebApplication14.DTOS.Response;

namespace WebApplication14.Repository
{
    public interface IbattleCommand
    {
        Task<Battlereponse> CrearAsync(CreateBattleRequest dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(int id, UpdateBattle dto);
    }
}
