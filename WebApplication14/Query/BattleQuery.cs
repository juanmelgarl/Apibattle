using Mapster;
using MapsterMapper;
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
      
        public async Task<List<Battlereponse>> GetAll(PaginationRequest pagination) 
        {
            if (!_Cache.TryGetValue(key, out List<Battlereponse> battles))
            {
                var entities = await _battleRepository.GetAll(pagination);

               battles = entities.Adapt<List<Battlereponse>>(); 
                    

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
                response = entities.Adapt<List<Battlereponse>>();
                SetCache(key, response);
            }
            return response;
        }
        public async Task<List<Battlereponse>> OrdenarAsc(PaginationRequest pagination)
        {
             if (!_Cache.TryGetValue(key,out List<Battlereponse> battles))
            {
                var entities = await _battleRepository.OrdenarAsc(pagination);
                battles = entities.Adapt<List<Battlereponse>>();
                SetCache(key, battles);
            }
            return battles;
         }
        public async Task<List<Battlereponse>> OrdenarDesc(PaginationRequest pagination)
        {
             if (!_Cache.TryGetValue(key,out List<Battlereponse> battles))
            {
                var entities = await _battleRepository.OrdenarDesc(pagination);
                battles = entities.Adapt<List<Battlereponse>>();
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
                battle = entities.Adapt<Battlereponse>();
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
