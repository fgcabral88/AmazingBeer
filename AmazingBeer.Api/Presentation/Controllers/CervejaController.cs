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
    }
}
