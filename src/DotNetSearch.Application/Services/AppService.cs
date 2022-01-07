using AutoMapper;
using DotNetSearch.Application.Contratos;
using DotNetSearch.Application.Interfaces;
using DotNetSearch.Domain.Entities;
using DotNetSearch.Domain.Interfaces;
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

        public async Task<IEnumerable<TContrato>> DapperGetAll()
        {
            return _mapper.Map<IEnumerable<TContrato>>(await _repository.DapperGetAll());
        }

        public async Task<TContrato> DapperGetById(Guid id)
        {
            return _mapper.Map<TContrato>(await _repository.DapperGetById(id));
        }
    }
}
