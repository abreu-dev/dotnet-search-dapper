using DotNetSearch.Application.Contratos;
using DotNetSearch.Application.Interfaces;
using DotNetSearch.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetSearch.API.Controllers.V2
{
    [ApiVersion("2")]
    public class AutorController : BaseController
    {
        private readonly IAutorAppService _autorAppService;

        public AutorController(IAutorAppService autorAppService)
        {
            _autorAppService = autorAppService;
        }

        [HttpGet("search")]
        public async Task<IEnumerable<AutorContrato>> Search([FromBody] SearchRequestModel searchRequestModel)
        {
            return await _autorAppService.DapperSearch(searchRequestModel);
        }

        [HttpGet]
        public async Task<IEnumerable<AutorContrato>> GetAll()
        {
            return await _autorAppService.DapperGetAll();
        }

        [HttpGet("{id:guid}")]
        public async Task<AutorContrato> GetById(Guid id)
        {
            return await _autorAppService.DapperGetById(id);
        }
    }
}
