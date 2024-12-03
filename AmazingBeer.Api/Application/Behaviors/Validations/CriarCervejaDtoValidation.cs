using AmazingBeer.Api.Application.Dtos.Cerveja;
using FluentValidation;

namespace AmazingBeer.Api.Application.Behaviors.Validations
{
    public class CriarCervejaDtoValidation : AbstractValidator<CriarCervejaDto>
    {
        private const string CampoObrigatorio = "O campo {PropertyName} é obrigatório.";

        public CriarCervejaDtoValidation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage(CampoObrigatorio);

            RuleFor(c => c.Estilo)
                .NotEmpty()
                .WithMessage(CampoObrigatorio);

            RuleFor(c => c.TeorAlcoolico)
                .NotNull()
                .WithMessage(CampoObrigatorio)
                .InclusiveBetween(0, 100)
                .WithMessage("O campo {PropertyName} deve estar entre 0 e 100.");

            RuleFor(c => c.Preco)
                .NotNull()
                .WithMessage(CampoObrigatorio)
                .GreaterThan(0)
                .WithMessage("O campo {PropertyName} deve ser maior que zero.");

            RuleFor(c => c.VolumeML)
                .NotNull()
                .WithMessage(CampoObrigatorio)
                .GreaterThan(0)
                .WithMessage("O campo {PropertyName} deve ser maior que zero.");

            RuleFor(c => c.FabricanteId)
                .NotEmpty()
                .WithMessage(CampoObrigatorio);

            RuleFor(c => c.UsuarioId)
                .NotEmpty()
                .WithMessage(CampoObrigatorio);
        }
    }
}
