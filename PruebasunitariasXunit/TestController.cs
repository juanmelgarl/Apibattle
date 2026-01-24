using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication14.Command;
using WebApplication14.Query;
using Moq;
using WebApplication14.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using WebApplication14.DTOS.Reques;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace PruebasunitariasXunit
{
    public class TestController
    {
        private readonly Mock<BattleQuery> _Query;
        private readonly Mock<BattleCommand> _COMMAND;
        private readonly BattleController _Controller;

        public TestController()
        {
             _Query = new Mock<BattleQuery>();
            _COMMAND  = new Mock <BattleCommand>();
            _Controller = new BattleController(_COMMAND.Object, _Query.Object);
        }
        [Fact]
        public async Task CrearBattlenull()
        {
            var result = await _Controller.Create(null);
             Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task CrearDtovalido()
        {
            var dto = new CreateBattleRequest { AmountAttackerSoldiers = 1400, AmountDefenderSoldiers = 2000, AttackerWon = false,  HasMayorCapture = true, HasMayorDeath = true, Name = "el guerrero de dios", Notes = "no se", Year = 1500};
            _COMMAND.Setup(c => c.CrearAsync(dto));
            var result = await _COMMAND.Object.CrearAsync(dto); 
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task ObtenerIdInvalido()
        {
            var result = await _Query.Object.GetById(0);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task ObtenerIDvalido()
        {
            _Query.Setup(q => q.GetById(10))
                .ReturnsAsync(new WebApplication14.DTOS.Response.Battlereponse
                {
                    Id = 12,
                    AmountAttackerSoldiers = 1233,
                    AmountDefenderSoldiers = 2321,
                    AttackerWon = true,
                    BattleTypeId = 30,
                    HasMayorCapture = false,
                    HasMayorDeath = true,
                    LocationId = 33,
                    Name = "no se",
                    Notes = "o",
                    Year = 3400,
                });
            var result = await _Query.Object.GetById(10);
          var type =  Assert.IsType<OkResult>(result);
           Assert.NotNull(type);
            
    }
        [Fact]
        public async Task ActualizarInvalido()
        {
            _COMMAND.Setup(q => q.UpdateAsync(1, It.IsAny<UpdateBattle>()))
                .ReturnsAsync(false);
            var result = await _COMMAND.Object.UpdateAsync(1,new UpdateBattle());
            Assert.False(result);
            Assert.IsType<NotFoundObjectResult>(result);  
         
        }
        [Fact]
        public async Task EliminarIDinvalido()
        {
            var result = await _Query.Object.GetById(0);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task EliminarBattleInvalido()
        {
            _COMMAND.Setup(q => q.DeleteAsync(0))
                .ReturnsAsync(false);
            var result = _COMMAND.Object.DeleteAsync(0);
            Assert.IsType<NotFoundObjectResult>(result.Result);

        }

        }
    }

