using DotNetSearch.Application.Contratos;
using DotNetSearch.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetSearch.API.Controllers.V1
{
    [ApiVersion("1")]
    public class AutorController : BaseController
    {
        private readonly IAutorAppService _autorAppService;

        public AutorController(IAutorAppService autorAppService)
        {
            _autorAppService = autorAppService;
        }

        [HttpGet]
        public async Task<IEnumerable<AutorContrato>> GetAll()
        {
            return await _autorAppService.GetAll();
        }

        [HttpGet("{id:guid}")]
        public async Task<AutorContrato> GetById(Guid id)
        {
            return await _autorAppService.GetById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AutorContrato autorContrato)
        {
            return Response(await _autorAppService.Add(autorContrato));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AutorContrato autorContrato)
        {
            return Response(await _autorAppService.Update(autorContrato));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            return Response(await _autorAppService.Remove(id));
        }
    }
}
