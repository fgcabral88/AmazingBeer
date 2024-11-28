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
        /// Retorna uma lista de cervejas cadastradas no banco de dados.
        /// </summary>
        /// <returns> Uma lista de cervejas disponíveis. </returns>
        [HttpGet]
        [Route("RetornarCervejas")]
        [SwaggerOperation(Summary = "Retornar todas as cervejas.", Description = "Retorna uma lista com todas as cervejas disponíveis no sistema.")]
        [SwaggerResponse(200, "Lista de cervejas retornada com sucesso.", typeof(IEnumerable<ListarCervejaDto>))]
        [SwaggerResponse(404, "Nenhuma cerveja encontrada.")]
        [SwaggerResponse(500, "Erro interno ao processar a solicitação.")]
        public async Task<IActionResult> RetornarCervejasAsync()
        {
            var response = await _cervejaService.RetornarCervejasAsync();

            if (!response.Success)
            {
                if (response.Data is null)
                    return NotFound(response.Message);

                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        /// <summary>
        /// Retorna uma cerveja pelo seu Id cadastrado no banco de dados.
        /// </summary>
        /// <returns> Uma cerveja pelo seus Id. </returns>
        [HttpGet]
        [Route("RetornarCervejaPorId/{id}")]
        [SwaggerOperation(Summary = "Retornar uma cerveja pelo seu Id.", Description = "Retorna uma cerveja pelo seu Id informado no parâmetro do endpoint.")]
        [SwaggerResponse(200, "Cerveja retornada com sucesso.", typeof(ListarCervejaDto))]
        [SwaggerResponse(404, "Nenhuma cerveja encontrada pelo Id informado.")]
        [SwaggerResponse(400, "Id informado é inválido ou ocorreu um erro durante o processamento.")]
        public async Task<IActionResult> RetornarCervejaPorIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("O Id informado é inválido.");

            var response = await _cervejaService.RetornarCervejaIdAsync(id);

            if (!response.Success)
            {
                if (response.Data is null)
                    return NotFound(response.Message);

                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        /// <summary>
        /// Cadastra uma nova cerveja no banco de dados.
        /// </summary>
        /// <param name="criarCervejaDto"></param>
        /// <returns> Cadastrar uma nova cerveja. </returns>
        [HttpPost]
        [Route("CadastrarCerveja")]
        [SwaggerOperation(Summary = "Cadastrar uma nova cerveja.", Description = "Cadastra uma nova cerveja no sistema.")]
        [SwaggerResponse(200, "Cerveja cadastrada com sucesso.", typeof(ListarCervejaDto))]
        [SwaggerResponse(400, "Ocorreu um erro durante o processamento.")]
        [SwaggerResponse(500, "Erro interno ao processar a solicitação.")]
        [SwaggerResponse(409, "Cerveja já cadastrada no sistema.")]
        public async Task<IActionResult> AdicionarCervejaAsync([FromBody] CriarCervejaDto criarCervejaDto)
        {
            var response = await _cervejaService.AdicionarCervejaAsync(criarCervejaDto);

            if (!response.Success)
            {
                if (response.Message.Contains("Cerveja já cadastrada.", StringComparison.OrdinalIgnoreCase))
                    return Conflict(response.Message);

                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

    }
}
