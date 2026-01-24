using FluentValidation.TestHelper;
using WebApplication14;
using WebApplication14.DTOS.Reques;
using WebApplication14.Validations;

namespace PruebasunitariasXunit

{
    public class UnitTest1
    {
        private readonly CreateBattleValidator _validator;

        public UnitTest1()
        {
             _validator = new CreateBattleValidator();
        }
        [Fact]
        public void Nombrevacio()
        {
            var dto = new CreateBattleRequest { Name = "" };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
        [Fact]
        public void yearError()
        {
            var dto = new CreateBattleRequest { Year = 900 };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Year);
        }
        [Fact]
        public void Dtovalido()
        {
            var dto = new CreateBattleRequest
            {
                Year = 2025,
                AmountAttackerSoldiers = 1233,
                AmountDefenderSoldiers = 1444,
                AttackerWon = false,
                HasMayorCapture = true,
                HasMayorDeath = true,
                Name = "el guerrero de dios",
                Notes = "Veterano"
            };
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveValidationErrorFor(x => new CreateBattleRequest());
        }
    }
}