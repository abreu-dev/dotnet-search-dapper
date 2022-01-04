using AutoMapper;
using DotNetSearch.Application.Contratos;
using DotNetSearch.Application.Interfaces;
using DotNetSearch.Domain.Entities;
using DotNetSearch.Domain.Interfaces;
using DotNetSearch.Infra.CrossCutting.DapperSearch.Helpers;
using DotNetSearch.Infra.CrossCutting.Search.Contratos;
using DotNetSearch.Infra.CrossCutting.Search.Enums;
using DotNetSearch.Infra.CrossCutting.Search.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetSearch.Application.Services
{
    public abstract class AppService<TContrato, TEntity> : IAppService<TContrato>
        where TContrato : Contrato
        where TEntity : Entity
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TEntity> _repository;

        protected AppService(IMapper mapper,
                             IRepository<TEntity> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<TContrato>> GetAll()
        {
            return _mapper.Map<IEnumerable<TContrato>>(await _repository.GetAll());
        }

        public async Task<TContrato> GetById(Guid id)
        {
            return _mapper.Map<TContrato>(await _repository.GetById(id));
        }

        public async Task<IEnumerable<TContrato>> Search(SearchContrato searchContrato)
        {
            if (searchContrato.Framework == SearchFramework.Linq)
            {
                var predicate = LinqLambdaBuilder.BuildPredicate<TEntity>(searchContrato);

                var searchResult = await _repository.Search(predicate,
                    searchContrato.Page,
                    searchContrato.PageSize);

                return _mapper.Map<IEnumerable<TContrato>>(searchResult);
            }
            else if (searchContrato.Framework == SearchFramework.Dapper)
            {
                var query = DapperQueryBuilder.BuildQuery<TEntity>(searchContrato);

                var searchResult = await _repository.Search(query);

                return _mapper.Map<IEnumerable<TContrato>>(searchResult);
            } 
            else
            {
                throw new Exception("Search Framework was not informed.");
            }
        }
    }
}
