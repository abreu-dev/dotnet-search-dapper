namespace DotNetSearch.Domain.Validators.CategoriaValidators
{
    public class UpdateCategoriaValidator : CategoriaValidator
    {
        public UpdateCategoriaValidator()
        {
            ValidateId();
            ValidateRequired();
        }
    }
}
