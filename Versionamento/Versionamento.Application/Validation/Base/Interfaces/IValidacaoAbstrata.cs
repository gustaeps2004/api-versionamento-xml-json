using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Versionamento.Application.Validation.Base.Interfaces
{
    public interface IValidacaoAbstrata<T>
    {
        Task<ValidationResult> ValidateAsync(ValidationContext<T> context, CancellationToken cancellationToken = default);
        Task<ValidationResult> Validator(ValidationContext<T> context, CancellationToken cancellationToken = default);
    }
}
