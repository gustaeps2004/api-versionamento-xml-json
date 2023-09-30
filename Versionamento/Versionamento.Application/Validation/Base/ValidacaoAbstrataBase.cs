using Versionamento.Application.Validation.ExceptionCustomizada;
using FluentValidation;
using FluentValidation.Results;

namespace Versionamento.Application.Validation.Base
{
    public class ValidacaoAbstrataBase<T> : AbstractValidator<T>
    {
        public Task<ValidationResult> Validator(ValidationContext<T> context, CancellationToken cancellationToken)
        {
            var validate = base.ValidateAsync(context, cancellationToken);
            if (validate.Result.IsValid)
                return validate;
            else
            {
                throw new ApplicationExceptionValidation(validate.Result.ToString());
            }
        }
    }
}
