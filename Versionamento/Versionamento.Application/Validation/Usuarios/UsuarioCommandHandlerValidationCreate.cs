using FluentValidation;
using FluentValidation.Results;
using Versionamento.Application.Usuarios.Commands;
using Versionamento.Application.Validation.Base;

namespace Versionamento.Application.Validation.Usuarios
{
    public class UsuarioCommandHandlerValidationCreate : ValidacaoAbstrataBase<UsuariosCreateCommand>
    {
        public override Task<ValidationResult> ValidateAsync(ValidationContext<UsuariosCreateCommand> context, CancellationToken cancellation = default)
        {
            RuleFor(x => x.Nome)
                .NotNull()
                .NotEmpty()
                .MaximumLength(255)
                .MinimumLength(5)
                .WithMessage("Nome é obrigatório");

            RuleFor(x => x.DocumentoFederal)
                .NotNull()
                .NotEmpty()
                .WithMessage("Documento federal é obrigatório");

            RuleFor(x => x.DocumentoFederal)
                .Must(DocumentoFederal => DocumentoFederal.Length == 11 || DocumentoFederal.Length == 14)
                .WithMessage("Documento federal incorreto, digite apenas cpf ou cnpj");

            RuleFor(x => x.DtNasc)
                .NotNull()
                .NotEmpty()
                .WithMessage("Data de nascimento é obrigatório");

            return Validator(context, cancellation);
        }
    }
}
