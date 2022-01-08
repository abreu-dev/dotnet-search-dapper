using AutoMapper;
using DotNetSearch.Application.Contratos;
using DotNetSearch.Application.Interfaces;
using DotNetSearch.Domain.Common;
using DotNetSearch.Domain.Entities;
using DotNetSearch.Domain.Interfaces;
using DotNetSearch.Domain.Validators.AutorValidators;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace DotNetSearch.Application.Services
{
    public class AutorAppService : AppService<AutorContrato, Autor>,
        IAutorAppService
    {
        private readonly IMapper _mapper;
        private readonly IAutorRepository _autorRepository;

        public AutorAppService(IMapper mapper,
                               IAutorRepository autorRepository)
            : base(mapper, autorRepository)
        {
            _mapper = mapper;
            _autorRepository = autorRepository;
        }

        public async Task<ValidationResult> Add(AutorContrato autorContrato)
        {
            var autor = _mapper.Map<Autor>(autorContrato);

            var validationResult = new AddAutorValidator().Validate(autor);
            if (validationResult.IsValid)
            {
                _autorRepository.Add(autor);
                await _autorRepository.UnitOfWork.Commit();
            }

            return validationResult;
        }

        public async Task<ValidationResult> Update(AutorContrato autorContrato)
        {
            var autor = _mapper.Map<Autor>(autorContrato);

            var validationResult = new UpdateAutorValidator().Validate(autor);
            if (validationResult.IsValid)
            {
                var dbEntity = await _autorRepository.GetById(autor.Id);
                if (dbEntity == null)
                {
                    validationResult.Errors.Add(new ValidationFailure("",
                        DomainMessages.NotFound.Format("Autor").Message));
                }
                else
                {
                    _mapper.Map(autor, dbEntity);
                    _autorRepository.Update(dbEntity);
                    await _autorRepository.UnitOfWork.Commit();
                }
            }

            return validationResult;
        }

        public async Task<ValidationResult> Remove(Guid id)
        {
            var validationResult = new RemoveAutorValidator().Validate(id);
            if (validationResult.IsValid)
            {
                var dbEntity = await _autorRepository.GetById(id);
                if (dbEntity == null)
                {
                    validationResult.Errors.Add(new ValidationFailure("",
                        DomainMessages.NotFound.Format("Autor").Message));
                }
                else
                {
                    _autorRepository.Remove(dbEntity);
                    await _autorRepository.UnitOfWork.Commit();
                }
            }

            return validationResult;
        }
    }
}
