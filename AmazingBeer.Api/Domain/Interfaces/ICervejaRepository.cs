using AmazingBeer.Api.Application.Dtos.Cerveja;
using AmazingBeer.Api.Application.Responses;

namespace AmazingBeer.Api.Domain.Interfaces
{
    public interface ICervejaRepository : IDisposable
    {
        Task<ResponseBase<IEnumerable<ListarCervejaDto>>> RetornarCervejasRepositorioAsync();

        Task<ResponseBase<ListarCervejaDto>> RetornarCervejasIdRepositorioAsync(Guid id);

        Task<ResponseBase<List<ListarCervejaDto>>> AdicionarCervejaRepositorioAsync(CriarCervejaDto criarCervejaDto);

        Task<ResponseBase<List<ListarCervejaDto>>> EditarCervejaRepositorioAsync(EditarCervejaDto editarCervejaDto);
    }
}
