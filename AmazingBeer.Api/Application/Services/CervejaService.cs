using AmazingBeer.Api.Application.Dtos.Cerveja;
using AmazingBeer.Api.Application.Interfaces;
using AmazingBeer.Api.Application.Responses;
using AmazingBeer.Api.Domain.Interfaces;
using AutoMapper;
using Serilog;

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

        public async Task<ResponseBase<IEnumerable<ListarCervejaDto>>> RetornarCervejasAsync()
        {
            try
            {
                // Busca os dados no repositório
                var cervejasResponse = await _cervejaRepository.RetornarCervejasRepositorioAsync();

                // Verifica se a operação foi bem-sucedida
                if (!cervejasResponse.Success || cervejasResponse.Data == null || !cervejasResponse.Data.Any())
                {
                    Log.Warning("Nenhuma cerveja encontrada na base de dados.");
                    return new ResponseBase<IEnumerable<ListarCervejaDto>>(success: false, message: "Nenhuma cerveja encontrada.", data: null);
                }

                // Mapeia os dados para o Dto
                var cervejas = _mapper.Map<List<ListarCervejaDto>>(cervejasResponse.Data);

                Log.Information($"Cervejas retornadas com sucesso. Total: {cervejas.Count}");
                return new ResponseBase<IEnumerable<ListarCervejaDto>>(success: true, message: "Cervejas retornadas com sucesso.", data: cervejas);
            }
            catch (Exception ex)
            {
                Log.Error($"Erro ao tentar retornar as cervejas: {ex.Message}", ex);
                return new ResponseBase<IEnumerable<ListarCervejaDto>>(success: false, message: "Ocorreu um erro ao processar sua solicitação.", data: null);
            }
        }

        public async Task<ResponseBase<ListarCervejaDto>> RetornarCervejaIdAsync(Guid id)
        {
            try
            {
                // Busca a cerveja pelo Id no repositório
                var cervejaIdResponse = await _cervejaRepository.RetornarCervejasIdRepositorioAsync(id);

                // Verifica se a operação foi bem-sucedida e se há dados
                if (!cervejaIdResponse.Success || cervejaIdResponse.Data == null)
                {
                    Log.Warning($"Cerveja com ID {id} não encontrada na base de dados.");
                    return new ResponseBase<ListarCervejaDto>(success: false, message: "Cerveja não encontrada na base de dados.", data: null);
                }

                // Mapeia os dados para o Dto
                var cervejaId = _mapper.Map<ListarCervejaDto>(cervejaIdResponse.Data);

                Log.Information($"Cerveja com Id: {id} retornada com sucesso.");
                return new ResponseBase<ListarCervejaDto>(success: true, message: "Cerveja retornada com sucesso.", data: cervejaId);
            }
            catch (Exception ex)
            {
                // Loga o erro com detalhes e retorna uma mensagem genérica
                Log.Error($"Erro ao retornar a cerveja com Id: {id}: {ex.Message}", ex);
                return new ResponseBase<ListarCervejaDto>(success: false, message: "Ocorreu um erro ao processar a solicitação.", data: null);
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
