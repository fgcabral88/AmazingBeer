using AmazingBeer.Api.Application.Dtos.Cerveja;
using AmazingBeer.Api.Application.Responses;

namespace AmazingBeer.Api.Domain.Interfaces
{
    public interface ICervejaRepository : IDisposable
    {
        Task<ResponseBase<IEnumerable<ListarCervejaDto>>> RetornarCervejasRepositorioAsync();
    }
}
