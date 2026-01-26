using Mapster;
using WebApplication14.DTOS.Reques;
using WebApplication14.DTOS.Response;
using WebApplication14.Models;
namespace WebApplication14.DTOS
{
    public class BattleMapping : IRegister
    {
           public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Battle, Battlereponse>();
            config.NewConfig<CreateBattleRequest, Battle>();
            config.NewConfig<UpdateBattle, Battle>();
        }
    }
}
