using DotNetSearch.Application.Contratos;
using DotNetSearch.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetSearch.API.Controllers.V2
{
    [ApiVersion("2")]
    public class LivroController : BaseController
    {
        private readonly ILivroAppService _livroAppService;

        public LivroController(ILivroAppService livroAppService)
        {
            _livroAppService = livroAppService;
        }

        [HttpGet]
        public async Task<IEnumerable<LivroContrato>> GetAll()
        {
            return await _livroAppService.DapperGetAll();
        }

        [HttpGet("{id:guid}")]
        public async Task<LivroContrato> GetById(Guid id)
        {
            return await _livroAppService.DapperGetById(id);
        }
    }
}
