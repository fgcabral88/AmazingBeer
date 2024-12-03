using AmazingBeer.Api.Application.Dtos.Cerveja;
using AmazingBeer.Api.Application.Interfaces;
using AmazingBeer.Api.Domain.Exceptions;
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
                    throw new CustomExceptions.NotFoundException(response.Message);

                throw new CustomExceptions.BadRequestException(response.Message);
            }

            return Ok(response);
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
                throw new CustomExceptions.BadRequestException("O Id informado é inválido.");

            var response = await _cervejaService.RetornarCervejaIdAsync(id);

            if (!response.Success)
            {
                if (response.Data is null)
                    throw new CustomExceptions.NotFoundException(response.Message);

                throw new CustomExceptions.BadRequestException(response.Message);
            }

            return Ok(response);
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
        [SwaggerResponse(409, "Cerveja já cadastrada no sistema.")]
        [SwaggerResponse(400, "Ocorreu um erro durante o processamento.")]
        [SwaggerResponse(500, "Erro interno ao processar a solicitação.")]
        public async Task<IActionResult> AdicionarCervejaAsync([FromBody] CriarCervejaDto criarCervejaDto)
        {
            if (!ModelState.IsValid)
                throw new CustomExceptions.BadRequestException(ModelState.ToString() ?? "Os dados informados são inválidos.");

            var response = await _cervejaService.AdicionarCervejaAsync(criarCervejaDto);

            if (!response.Success)
            {
                if (response.Data is null)
                    throw new CustomExceptions.ConflictException(response.Message);

                throw new CustomExceptions.BadRequestException(response.Message);
            }

            return Ok(response);
        }

        /// <summary>
        /// Edita uma cerveja pelo seu Id cadastrado no banco de dados.
        /// </summary>
        /// <param name="editarCervejaDto"></param>
        /// <returns> Editar uma cerveja. </returns>
        [HttpPut]
        [Route("EditarCerveja")]
        [SwaggerOperation(Summary = "Editar uma cerveja.", Description = "Edita uma cerveja pelo seu Id informado no parâmetro do endpoint.")]
        [SwaggerResponse(200, "Cerveja editada com sucesso.", typeof(ListarCervejaDto))]
        [SwaggerResponse(404, "Nenhuma cerveja encontrada pelo Id informado.")]
        [SwaggerResponse(400, "Ocorreu um erro durante o processamento.")]
        [SwaggerResponse(500, "Erro interno ao processar a solicitação.")]
        public async Task<IActionResult> EditarCervejaAsync([FromBody] EditarCervejaDto editarCervejaDto)
        {
            if(editarCervejaDto is null)
                throw new CustomExceptions.BadRequestException("Os dados informados são inválidos.");

            var response = await _cervejaService.EditarCervejaAsync(editarCervejaDto);

            return Ok(response);
        }

        /// <summary>
        /// Deleta uma cerveja pelo seu Id cadastrado no banco de dados.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Deletar uma cerveja. </returns>
        [HttpDelete]
        [Route("DeletarCerveja/{id}")]
        [SwaggerOperation(Summary = "Deletar uma cerveja.", Description = "Deletar uma cerveja pelo seu Id informado no parâmetro do endpoint.")]
        [SwaggerResponse(200, "Cerveja deletada com sucesso.", typeof(ListarCervejaDto))]
        [SwaggerResponse(404, "Nenhuma cerveja encontrada pelo Id informado.")]
        [SwaggerResponse(400, "Ocorreu um erro durante o processamento.")]
        [SwaggerResponse(500, "Erro interno ao processar a solicitação.")]
        public async Task<IActionResult> DeletarCervejaAsync(Guid id)
        {
            if(id == Guid.Empty)
                return BadRequest("O Id informado é inválido.");

            var response = await _cervejaService.DeletarCervejaAsync(id);

            return Ok(response);
        }
    }
}
