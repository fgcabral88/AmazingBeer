using AmazingBeer.Api.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AmazingBeer.Api.Presentation.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CervejaController : ControllerBase
    {
        private readonly ICervejaService _cervejaService;
        
        public CervejaController(ICervejaService cervejaService)
        {
            _cervejaService = cervejaService;
        }

        /// <summary>
        /// Endpoint - Retorna todas as cervejas.
        /// </summary>
        /// <remarks>
        /// Este endpoint retorna uma lista de todas as cervejas cadastradas.
        /// </remarks>
        /// <returns> Lista de cervejas. </returns>
        [HttpGet]
        [Route("RetornarCervejas")]
        public async Task<IActionResult> RetornarCervejasAsync()
        {
            var cervejas = await _cervejaService.RetornarCervejasAsync();

            return Ok(cervejas);
        }

        /// <summary>
        /// Endpoint - Retorna uma cerveja por Id.
        /// </summary>
        /// <remarks>
        /// Este endpoint retorna um objeto da cerveja pelo seu Id cadastrado.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns> Retorno de uma cerveja </returns>
        [HttpGet]
        [Route("RetornarCervejaPorId/{id}")]
        public async Task<IActionResult> RetornarCervejaPorIdAsync(Guid id)
        {
            var cervejaId = await _cervejaService.RetornarCervejaIdAsync(id);

            return Ok(cervejaId);
        }
    }
}
