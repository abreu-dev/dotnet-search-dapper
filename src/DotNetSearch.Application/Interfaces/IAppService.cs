using DotNetSearch.Application.Contratos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetSearch.Application.Interfaces
{
    public interface IAppService<TContrato>
        where TContrato : Contrato
    {
        Task<IEnumerable<TContrato>> GetAll();
        Task<TContrato> GetById(Guid id);
    }
}
