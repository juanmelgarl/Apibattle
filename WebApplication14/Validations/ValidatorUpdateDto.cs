using FluentValidation;
using WebApplication14.DTOS.Reques;

namespace WebApplication14.Validations
{
    public class ValidatorUpdateDto : AbstractValidator<UpdateBattle>
    {
        public ValidatorUpdateDto() 
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("No puede ser nulo el nombre");
            RuleFor(x => x.AttackerWon).NotEmpty().NotNull().WithMessage("No puede ser nulo");
            RuleFor(x => x.Notes).NotNull().NotEmpty().WithMessage("No puede ser nulo");
            RuleFor(x => x.Year).NotEmpty().NotNull().GreaterThanOrEqualTo(1000).WithMessage("EL AÑO DEBE ser mayor a 1000");
            RuleFor(x => x.HasMayorDeath).NotEmpty().NotNull();
            RuleFor(x => x.AmountDefenderSoldiers).NotNull().NotEmpty().GreaterThanOrEqualTo(1).WithMessage("Debe ser mayor a 1 la defensa de soldados");
            RuleFor(x => x.AmountAttackerSoldiers).NotEmpty().NotNull().GreaterThanOrEqualTo(1).WithMessage("Ingresa un valor valido para el daño de soldados");
            RuleFor(x => x.HasMayorCapture).NotEmpty().NotNull();
         
        }
    }
}
