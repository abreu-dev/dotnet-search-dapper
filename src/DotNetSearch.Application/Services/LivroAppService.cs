using AutoMapper;
using DotNetSearch.Application.Contratos;
using DotNetSearch.Application.Interfaces;
using DotNetSearch.Domain.Common;
using DotNetSearch.Domain.Entities;
using DotNetSearch.Domain.Interfaces;
using DotNetSearch.Domain.Validators.LivroValidators;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace DotNetSearch.Application.Services
{
    public class LivroAppService : AppService<LivroContrato, Livro>,
        ILivroAppService
    {
        private readonly IMapper _mapper;
        private readonly ILivroRepository _livroRepository;

        public LivroAppService(IMapper mapper,
                               ILivroRepository livroRepository)
            : base(mapper, livroRepository)
        {
            _mapper = mapper;
            _livroRepository = livroRepository;
        }

        public async Task<ValidationResult> Add(LivroContrato livroContrato)
        {
            var livro = _mapper.Map<Livro>(livroContrato);

            var validationResult = new AddLivroValidator().Validate(livro);
            if (validationResult.IsValid)
            {
                _livroRepository.Add(livro);
                await _livroRepository.UnitOfWork.Commit();
            }

            return validationResult;
        }

        public async Task<ValidationResult> Update(LivroContrato livroContrato)
        {
            var livro = _mapper.Map<Livro>(livroContrato);

            var validationResult = new UpdateLivroValidator().Validate(livro);
            if (validationResult.IsValid)
            {
                var dbEntity = await _livroRepository.GetById(livro.Id);
                if (dbEntity == null)
                {
                    validationResult.Errors.Add(new ValidationFailure("",
                        DomainMessages.NotFound.Format("Livro").Message));
                }
                else
                {
                    _mapper.Map(livro, dbEntity);
                    _livroRepository.Update(dbEntity);
                    await _livroRepository.UnitOfWork.Commit();
                }
            }

            return validationResult;
        }

        public async Task<ValidationResult> Remove(Guid id)
        {
            var validationResult = new RemoveLivroValidator().Validate(id);
            if (validationResult.IsValid)
            {
                var dbEntity = await _livroRepository.GetById(id);
                if (dbEntity == null)
                {
                    validationResult.Errors.Add(new ValidationFailure("",
                        DomainMessages.NotFound.Format("Livro").Message));
                }
                else
                {
                    _livroRepository.Remove(dbEntity);
                    await _livroRepository.UnitOfWork.Commit();
                }
            }

            return validationResult;
        }
    }
}
