using AmazingBeer.Api.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        /// Obtém uma lista de cervejas.
        /// </summary>
        /// <returns> Uma lista de cervejas disponíveis. </returns>
        [HttpGet]
        [Route("RetornarCervejas")]
        [SwaggerOperation(Summary = "Obter todas as cervejas.", Description = "Retorna uma lista com todas as cervejas disponíveis no sistema.")]
        [SwaggerResponse(200, "Lista de cervejas retornada com sucesso.")]
        [SwaggerResponse(404, "Nenhuma cerveja encontrada.")]
        public async Task<IActionResult> RetornarCervejasAsync()
        {
            var cervejas = await _cervejaService.RetornarCervejasAsync();

            return Ok(cervejas);
        }
        
        /// <summary>
        /// Obtém uma cerveja pelo seu Id.
        /// </summary>
        /// <returns> Uma cerveja pelo seus Id. </returns>
        [HttpGet]
        [Route("RetornarCervejaPorId/{id}")]
        [SwaggerOperation(Summary = "Obter uma cerveja pelo seu Id.", Description = "Retorna uma cerveja pelo seu Id informado no parâmetro do endpoint.")]
        [SwaggerResponse(200, "Cerveja retornada com sucesso.")]
        [SwaggerResponse(404, "Nenhuma cerveja encontrada pelo seu Id informado no parâmetro do endpoint.")]
        public async Task<IActionResult> RetornarCervejaPorIdAsync(Guid id)
        {
            var cervejaId = await _cervejaService.RetornarCervejaIdAsync(id);

            return Ok(cervejaId);
        }
    }
}
