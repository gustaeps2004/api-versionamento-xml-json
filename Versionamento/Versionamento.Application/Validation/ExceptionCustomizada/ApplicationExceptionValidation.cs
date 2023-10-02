namespace Versionamento.Application.Validation.ExceptionCustomizada
{
    public class ApplicationExceptionValidation : Exception
    {
        public ApplicationExceptionValidation(string error) : base(error)
        { }
    }
}
