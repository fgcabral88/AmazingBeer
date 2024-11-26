using AmazingBeer.Api.Application.Dtos.Cerveja;
using AmazingBeer.Api.Application.Interfaces;
using AmazingBeer.Api.Application.Responses;
using AmazingBeer.Api.Domain.Interfaces;
using AutoMapper;

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
                {
                    return new ResponseBase<ListarCervejaDto>(success: false, message: cervejasResponse.Message, data: null);
                }

                var cervejas = _mapper.Map<IEnumerable<ListarCervejaDto>>(cervejasResponse.Data);

                return new ResponseBase<ListarCervejaDto>(success: true, message: "Cervejas retornada com sucesso.", data: cervejas.First());
            }
            catch(Exception ex)
            {
                return new ResponseBase<ListarCervejaDto>(success: false, message: ex.Message, data: null);
            }
        }

        public async Task<ResponseBase<ListarCervejaDto>> RetornarCervejaIdAsync(Guid Id)
        {
            try
            {
                var cervejaIdResponse = await _cervejaRepository.RetornarCervejasIdRepositorioAsync(Id);

                if (!cervejaIdResponse.Success)
                {
                    return new ResponseBase<ListarCervejaDto>(success: false, message: cervejaIdResponse.Message, data: null);
                }

                var cervejaId = _mapper.Map<ListarCervejaDto>(cervejaIdResponse.Data);

                return new ResponseBase<ListarCervejaDto>(success: true, message: "Cerveja Id retornada com sucesso.", data: cervejaId);
            }
            catch (Exception ex)
            {
                return new ResponseBase<ListarCervejaDto>(success: false, message: ex.Message, data: null);
            }
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
