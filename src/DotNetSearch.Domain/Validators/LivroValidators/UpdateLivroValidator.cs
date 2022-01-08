namespace DotNetSearch.Domain.Validators.LivroValidators
{
    public class UpdateLivroValidator : LivroValidator
    {
        public UpdateLivroValidator()
        {
            ValidateId();
            ValidateRequired();
        }
    }
}
