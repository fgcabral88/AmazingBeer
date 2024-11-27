using AmazingBeer.Api.Application.Dtos.Cerveja;
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
        [SwaggerResponse(200, "Lista de cervejas retornada com sucesso.", typeof(IEnumerable<ListarCervejaDto>))]
        [SwaggerResponse(404, "Nenhuma cerveja encontrada.")]
        [SwaggerResponse(500, "Erro interno ao processar a solicitação.")]
        public async Task<IActionResult> RetornarCervejasAsync()
        {
            var response = await _cervejaService.RetornarCervejasAsync();

            if (!response.Success)
            {
                if (response.Data == null)
                    return NotFound(response.Message);

                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }


        /// <summary>
        /// Obtém uma cerveja pelo seu Id.
        /// </summary>
        /// <returns> Uma cerveja pelo seus Id. </returns>
        [HttpGet]
        [Route("RetornarCervejaPorId/{id}")]
        [SwaggerOperation(Summary = "Obter uma cerveja pelo seu Id.", Description = "Retorna uma cerveja pelo seu Id informado no parâmetro do endpoint.")]
        [SwaggerResponse(200, "Cerveja retornada com sucesso.", typeof(ListarCervejaDto))]
        [SwaggerResponse(404, "Nenhuma cerveja encontrada pelo Id informado.")]
        [SwaggerResponse(400, "Id informado é inválido ou ocorreu um erro durante o processamento.")]
        public async Task<IActionResult> RetornarCervejaPorIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("O Id informado é inválido.");
            }

            var response = await _cervejaService.RetornarCervejaIdAsync(id);

            if (!response.Success)
            {
                if (response.Data == null)
                    return NotFound(response.Message);

                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

    }
}
