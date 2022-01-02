using AutoMapper;
using DotNetSearch.Application.Contratos;
using DotNetSearch.Application.Interfaces;
using DotNetSearch.Domain.Common;
using DotNetSearch.Domain.Entities;
using DotNetSearch.Domain.Interfaces;
using DotNetSearch.Domain.Validators.CategoriaValidators;
using DotNetSearch.Infra.CrossCutting.LinqSearch.Contratos;
using DotNetSearch.Infra.CrossCutting.LinqSearch.Helpers;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DotNetSearch.Application.Services
{
    public class CategoriaAppService : AppService<CategoriaContrato, Categoria>,
        ICategoriaAppService
    {
        private readonly IMapper _mapper;
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaAppService(IMapper mapper,
                                   ICategoriaRepository categoriaRepository)
            : base(mapper, categoriaRepository) 
        {
            _mapper = mapper;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<ValidationResult> Add(CategoriaContrato categoriaContrato)
        {
            var categoria = _mapper.Map<Categoria>(categoriaContrato);

            var validationResult = new AddCategoriaValidator().Validate(categoria);
            if (validationResult.IsValid)
            {
                _categoriaRepository.Add(categoria);
                await _categoriaRepository.UnitOfWork.Commit();
            }

            return validationResult;
        }

        public async Task<ValidationResult> Update(CategoriaContrato categoriaContrato)
        {
            var categoria = _mapper.Map<Categoria>(categoriaContrato);

            var validationResult = new UpdateCategoriaValidator().Validate(categoria);
            if (validationResult.IsValid)
            {
                var dbEntity = await _categoriaRepository.GetById(categoria.Id);
                if (dbEntity == null)
                {
                    validationResult.Errors.Add(new ValidationFailure("", 
                        DomainMessages.NotFound.Format("Categoria").Message));
                }
                else
                {
                    _mapper.Map(categoria, dbEntity);
                    _categoriaRepository.Update(dbEntity);
                    await _categoriaRepository.UnitOfWork.Commit();
                }
            }

            return validationResult;
        }

        public async Task<ValidationResult> Remove(Guid id)
        {
            var validationResult = new RemoveCategoriaValidator().Validate(id);
            if (validationResult.IsValid)
            {
                var dbEntity = await _categoriaRepository.GetById(id);
                if (dbEntity == null)
                {
                    validationResult.Errors.Add(new ValidationFailure("", 
                        DomainMessages.NotFound.Format("Categoria").Message));
                }
                else
                {
                    _categoriaRepository.Remove(dbEntity);
                    await _categoriaRepository.UnitOfWork.Commit();
                }
            }

            return validationResult;
        }

        public async Task<IEnumerable<CategoriaContrato>> Search(SearchContrato searchContrato)
        {
            Expression<Func<Categoria, bool>> predicate = LinqLambdaBuilder.BuildPredicate<Categoria>(searchContrato);

            var searchResult = await _categoriaRepository.CustomSearch(searchContrato.Page,
                searchContrato.PageSize, predicate);

            return _mapper.Map<IEnumerable<CategoriaContrato>>(searchResult);
        }
    }
}
