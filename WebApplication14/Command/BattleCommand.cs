using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Caching.Memory;
using System.Runtime.InteropServices;
using WebApplication14.DTOS.Reques;
using WebApplication14.DTOS.Response;
using WebApplication14.Models;
using WebApplication14.Repository;

namespace WebApplication14.Command
{
    public class BattleCommand : IbattleCommand
    {
        private readonly IBattleRepository _Repo;
        private readonly IMemoryCache _cache;
        private readonly ILogger<Battlereponse> _logger;
        private const string CachePrefix = "battle_";


        public BattleCommand(IBattleRepository repo,IMemoryCache cache,ILogger<Battlereponse> logger)
        {
             _Repo = repo;
            _cache = cache;
            _logger = logger;
        }
        public async Task<Battlereponse> CrearAsync(CreateBattleRequest dto)
        {
            var battle = dto.Adapt<Battle>();
            await _Repo.Create(battle);
            await _Repo.Save();
            _cache.Remove($"{CachePrefix}all");
            _cache.Remove($"{CachePrefix}asc");
            _cache.Remove($"{CachePrefix}desc");
            _logger.LogInformation($"Se ha creado Una batalla - Nombre {dto.Name}");
            return new Battlereponse
            {
                Year = battle.Year,
                Notes = battle.Notes,
                AmountAttackerSoldiers = battle.AmountAttackerSoldiers,
                AmountDefenderSoldiers = battle.AmountDefenderSoldiers,
                AttackerWon = battle.AttackerWon,
                BattleTypeId = battle.BattleTypeId,
                HasMayorCapture = battle.HasMayorCapture,
                HasMayorDeath = battle.HasMayorDeath,
                Id = battle.Id,
                LocationId = battle.LocationId,
                Name = battle.Name,
            };

        }
        public async Task<bool> UpdateAsync(int id,UpdateBattle dto)
        {
            var battle = await _Repo.GetId(id);
            if (battle is null)
            {
                return false;
            }
            battle = dto.Adapt<Battle>();
            await _Repo.Update(battle);
            await _Repo.Save();
            _cache.Remove($"{CachePrefix}all");
            _cache.Remove($"{CachePrefix}asc");
            _cache.Remove($"{CachePrefix}desc");
            _cache.Remove($"{CachePrefix}id_{id}");
            _logger.LogInformation($"Se ha actualizado una batalla con el id {id}");
            return true;
          
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var battle = await _Repo.GetId(id);
            if (battle is null)
            {
                return false;
            }
          await  _Repo.Delete(battle);
            await _Repo.Save();
            _cache.Remove($"{CachePrefix}all");
            _cache.Remove($"{CachePrefix}asc");
            _cache.Remove($"{CachePrefix}desc");
            _cache.Remove($"{CachePrefix}id_{id}");
            _logger.LogInformation($"Se ha borrado una batalla con el id {id}");
            return true;

        }
    }
}
