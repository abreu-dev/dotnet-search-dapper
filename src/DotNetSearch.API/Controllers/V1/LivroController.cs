using DotNetSearch.Application.Contratos;
using DotNetSearch.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetSearch.API.Controllers.V1
{
    [ApiVersion("1")]
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
            return await _livroAppService.GetAll();
        }

        [HttpGet("{id:guid}")]
        public async Task<LivroContrato> GetById(Guid id)
        {
            return await _livroAppService.GetById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] LivroContrato livroContrato)
        {
            return Response(await _livroAppService.Add(livroContrato));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] LivroContrato livroContrato)
        {
            return Response(await _livroAppService.Update(livroContrato));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            return Response(await _livroAppService.Remove(id));
        }
    }
}
