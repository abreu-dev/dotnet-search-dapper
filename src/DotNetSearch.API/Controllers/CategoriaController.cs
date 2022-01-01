using DotNetSearch.Application.Contratos;
using DotNetSearch.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetSearch.API.Controllers
{
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
            return await _categoriaAppService.GetAll();
        }

        [HttpGet("{id:guid}")]
        public async Task<CategoriaContrato> GetById(Guid id)
        {
            return await _categoriaAppService.GetById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CategoriaContrato categoriaContrato)
        {
            return Response(await _categoriaAppService.Add(categoriaContrato));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CategoriaContrato categoriaContrato)
        {
            return Response(await _categoriaAppService.Update(categoriaContrato));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            return Response(await _categoriaAppService.Remove(id));
        }
    }
}
