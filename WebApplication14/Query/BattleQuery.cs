using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Caching.Memory;
using WebApplication14.DTOS.Response;
using WebApplication14.Models;
using WebApplication14.Pagination;
using WebApplication14.Repository;

namespace WebApplication14.Query
{
    public class BattleQuery : IBattleQuery
    {
        private readonly IBattleRepository _battleRepository;
        private readonly IMemoryCache _Cache;
        private string key = "listaproductos";
        public BattleQuery(IBattleRepository battleRepository,IMemoryCache cache)
        {
            _battleRepository = battleRepository;
            _Cache = cache;
        }
        private Battlereponse Toresponse(Battle d) => new Battlereponse
        {
            AmountAttackerSoldiers = d.AmountAttackerSoldiers,
            AmountDefenderSoldiers = d.AmountDefenderSoldiers,
            AttackerWon = d.AttackerWon,
            BattleTypeId = d.BattleTypeId,
            HasMayorCapture = d.HasMayorCapture,
            HasMayorDeath = d.HasMayorDeath,
            Id = d.Id,
            LocationId = d.LocationId,
            Name = d.Name,
            Notes = d.Notes,
            Year = d.Year,
        };
        public async Task<List<Battlereponse>> GetAll(PaginationRequest pagination) 
        {
            if (!_Cache.TryGetValue(key, out List<Battlereponse> battles))
            {
                var entities = await _battleRepository.GetAll(pagination);

                battles = entities.Select(b => new Battlereponse
                {
                    Year = b.Year,
                    AmountAttackerSoldiers = b.AmountAttackerSoldiers,
                    AmountDefenderSoldiers = b.AmountDefenderSoldiers,
                    AttackerWon = b.AttackerWon,
                    HasMayorCapture = b.HasMayorCapture,
                    HasMayorDeath = b.HasMayorDeath,
                    Name = b.Name,
                    Notes = b.Notes,
                    BattleTypeId = b.BattleTypeId,
                    Id = b.Id,
                    LocationId = b.LocationId,

                }).ToList();

                _Cache.Set(key, battles,
                    new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                        SlidingExpiration = TimeSpan.FromMinutes(5)
                    });
            }

            return battles;

        }

        public async Task<List<Battlereponse>> FiltrarPornombre(string nombre, PaginationRequest pagination)
        {
            if (!_Cache.TryGetValue(key,out List<Battlereponse> response))
            {
                var entities = await _battleRepository.Filtrarpornombre(nombre, pagination);
                response = entities.Select(Toresponse).ToList();
                SetCache(key, response);
            }
            return response;
        }
        public async Task<List<Battlereponse>> OrdenarAsc(PaginationRequest pagination)
        {
             if (!_Cache.TryGetValue(key,out List<Battlereponse> battles))
            {
                var entities = await _battleRepository.OrdenarAsc(pagination);
                battles = entities.Select(Toresponse).ToList();
                SetCache(key, battles);
            }
            return battles;
         }
        public async Task<List<Battlereponse>> OrdenarDesc(PaginationRequest pagination)
        {
             if (!_Cache.TryGetValue(key,out List<Battlereponse> battles))
            {
                var entities = await _battleRepository.OrdenarDesc(pagination);
                battles = entities.Select(Toresponse).ToList();
                SetCache(key, battles);
            }
            return battles;
        }
        public async Task<Battlereponse> GetById(int id)
        {
            if (!_Cache.TryGetValue(key,out Battlereponse battle))
            {
                var entities = await _battleRepository.GetId(id);
                if (entities is null)
                    return null;
                battle = Toresponse(entities);
                SetCache(key, battle);
            }
            return battle;
         }

        private void SetCache<T>(string key, T value)
        {
            _Cache.Set(
                key,
                value,
                new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                });
        }



    }
}
