using FluentValidation;
using FluentValidation.Results;
using Versionamento.Application.Usuarios.Commands;
using Versionamento.Application.Validation.Base;

namespace Versionamento.Application.Validation.Usuarios
{
    public class UsuarioCommandHandlerValidationUpdate : ValidacaoAbstrataBase<UsuariosUpdateCommand>
    {
        public override Task<ValidationResult> ValidateAsync(ValidationContext<UsuariosUpdateCommand> context, CancellationToken cancellation = default)
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
