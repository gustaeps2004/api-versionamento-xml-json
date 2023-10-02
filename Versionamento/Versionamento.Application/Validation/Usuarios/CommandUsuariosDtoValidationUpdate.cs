using FluentValidation;
using FluentValidation.Results;
using Versionamento.Application.DTOs;
using Versionamento.Application.Validation.Base;

namespace Versionamento.Application.Validation.Usuarios
{
    public class CommandUsuariosDtoValidationUpdate : ValidacaoAbstrataBase<UsuariosDto>
    {
        public override Task<ValidationResult> ValidateAsync(ValidationContext<UsuariosDto> context, CancellationToken cancellation = default)
        {
            RuleFor(x => x.Nome)
                .NotNull()
                .NotEmpty()
                .MaximumLength(255)
                .MinimumLength(5)
                .WithMessage("Nome é obrigatório");

            RuleFor(x => x.DtNasc)
                .NotNull()
                .NotEmpty()
                .WithMessage("Data de nascimento é obrigatório");

            return Validator(context, cancellation);
        }
    }
}
