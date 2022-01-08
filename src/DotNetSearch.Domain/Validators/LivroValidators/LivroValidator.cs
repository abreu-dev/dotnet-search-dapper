using DotNetSearch.Domain.Common;
using DotNetSearch.Domain.Entities;
using FluentValidation;

namespace DotNetSearch.Domain.Validators.LivroValidators
{
    public abstract class LivroValidator : Validator<Livro>
    {
        protected void ValidateRequired()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty()
                .WithMessage(DomainMessages.RequiredField.Format("Título").Message);

            RuleFor(x => x.NumeroPaginas)
                .NotEmpty()
                .WithMessage(DomainMessages.RequiredField.Format("NumeroPaginas").Message);

            RuleFor(x => x.DataPublicacao)
                .NotEmpty()
                .WithMessage(DomainMessages.RequiredField.Format("DataPublicacao").Message);

            RuleFor(x => x.AutorId)
                .NotEmpty()
                .WithMessage(DomainMessages.RequiredField.Format("AutorId").Message);

            RuleForEach(x => x.Categorias)
                .SetValidator(x => new LivroCategoriaValidator());
        }
    }

    public class LivroCategoriaValidator : Validator<LivroCategoria>
    {
        public LivroCategoriaValidator()
        {
            RuleFor(x => x.CategoriaId)
                .NotEmpty()
                .WithMessage(DomainMessages.RequiredField.Format("CategoriaId").Message);
        }
    }
}
