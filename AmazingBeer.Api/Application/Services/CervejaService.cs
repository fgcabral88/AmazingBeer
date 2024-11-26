using AmazingBeer.Api.Application.Dtos.Cerveja;
using AmazingBeer.Api.Application.Interfaces;
using AmazingBeer.Api.Application.Responses;
using AmazingBeer.Api.Domain.Interfaces;
using AutoMapper;
using System.Runtime.CompilerServices;

namespace AmazingBeer.Api.Application.Services
{
    public class CervejaService : ICervejaService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ICervejaRepository _cervejaRepository;

        public CervejaService(IConfiguration configuration, IMapper mapper, ICervejaRepository cervejaRepository)
        {
            _configuration = configuration;
            _mapper = mapper;
            _cervejaRepository = cervejaRepository;
        }


        public async Task<ResponseBase<ListarCervejaDto>> RetornarCervejasAsync()
        {
            try
            {
                var cervejasResponse = await _cervejaRepository.RetornarCervejasRepositorioAsync();

                if (!cervejasResponse.Success)
                    return new ResponseBase<ListarCervejaDto>(success: false, message: cervejasResponse.Message, data: null);

                var cervejasDto = _mapper.Map<IEnumerable<ListarCervejaDto>>(cervejasResponse.Data);

                return new ResponseBase<ListarCervejaDto>(success: true, message: "Cervejas recuperadas com sucesso.", data: cervejasDto.First());
            }
            catch(Exception ex)
            {
                return new ResponseBase<ListarCervejaDto>(success: false, message: ex.Message, data: null);
            }
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
