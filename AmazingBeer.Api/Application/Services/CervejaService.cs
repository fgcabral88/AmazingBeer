using AmazingBeer.Api.Application.Dtos.Cerveja;
using AmazingBeer.Api.Application.Interfaces;
using AmazingBeer.Api.Application.Responses;

namespace AmazingBeer.Api.Application.Services
{
    public class CervejaService : ICervejaService
    {
        public CervejaService() 
        {
        
        }


        public Task<ResponseBase<ListarCervejaDto>> RetornarCervejasAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBase<ListarCervejaDto>> RetornarCervejaIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBase<ListarCervejaDto>> AdicionarCervejaAsync(CriarCervejaDto cervejaCriarDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBase<ListarCervejaDto>> EditarCervejaAsync(EditarCervejaDto cervejaEditarDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBase<ListarCervejaDto>> DeletarCervejaAsync(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
