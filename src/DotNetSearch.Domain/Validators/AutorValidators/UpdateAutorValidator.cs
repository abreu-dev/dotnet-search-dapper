namespace DotNetSearch.Domain.Validators.AutorValidators
{
    public class UpdateAutorValidator : AutorValidator
    {
        public UpdateAutorValidator()
        {
            ValidateId();
            ValidateRequired();
        }
    }
}
