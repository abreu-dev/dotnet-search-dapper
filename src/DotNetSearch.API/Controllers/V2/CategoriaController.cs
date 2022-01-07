using DotNetSearch.Application.Contratos;
using DotNetSearch.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetSearch.API.Controllers.V2
{
    [ApiVersion("2")]
    public class CategoriaController : BaseController
    {
        private readonly ICategoriaAppService _categoriaAppService;

        public CategoriaController(ICategoriaAppService categoriaAppService)
        {
            _categoriaAppService = categoriaAppService;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoriaContrato>> GetAll()
        {
            return await _categoriaAppService.DapperGetAll();
        }

        [HttpGet("{id:guid}")]
        public async Task<CategoriaContrato> GetById(Guid id)
        {
            return await _categoriaAppService.DapperGetById(id);
        }
    }
}
