using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApplication14.Command;
using WebApplication14.Controllers;
using WebApplication14.DTOS.Reques;
using WebApplication14.DTOS.Response;
using WebApplication14.Query;
using WebApplication14.Repository;

namespace PruebasunitariasXunit
{
    public class TestController
    {
        private readonly Mock<IBattleQuery> _query;
        private readonly Mock<IbattleCommand> _command;
        private readonly BattleController _controller;

        public TestController()
        {
            _query = new Mock<IBattleQuery>();
            _command = new Mock<IbattleCommand>();
            _controller = new BattleController(_command.Object, _query.Object);
        }


        [Fact]
        public async Task CrearBattlenull()
        {
            var result = await _controller.Create(null);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task CrearDtovalido()
        {
            var dto = new CreateBattleRequest
            {
                AmountAttackerSoldiers = 1400,
                AmountDefenderSoldiers = 2000,
                AttackerWon = false,
                HasMayorCapture = true,
                HasMayorDeath = true,
                Name = "el guerrero de dios",
                Notes = "no se",
                Year = 1500
            };

            _command.Setup(c => c.CrearAsync(It.IsAny<CreateBattleRequest>()))
                .ReturnsAsync(new Battlereponse
                {
                    Id = 3,
                    Name = "juan"
                });

            var result = await _controller.Create(dto);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<Battlereponse>(ok.Value);
        }


        [Fact]
        public async Task ObtenerIdInvalido()
        {
            var result = await _controller.Getporid(0);

            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task ObtenerIDvalido()
        {
            _query.Setup(q => q.GetById(10))
                .ReturnsAsync(new Battlereponse
                {
                    Id = 10,
                    Name = "batalla"
                });

            var result = await _controller.Getporid(10);

            var ok = Assert.IsType<OkObjectResult>(result.Result); 
            Assert.IsType<Battlereponse>(ok.Value);
        }

        [Fact]
        public async Task ActualizarInvalido()
        {
            _command.Setup(c => c.UpdateAsync(1, It.IsAny<UpdateBattle>()))
                .ReturnsAsync(false);

            var result = await _controller.Update(1, new UpdateBattle());

            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async Task EliminarBattleInvalido()
        {
            _command.Setup(c => c.DeleteAsync(1))
                .ReturnsAsync(false);

            var result = await _controller.Delete(1);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
