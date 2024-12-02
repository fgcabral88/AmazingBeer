﻿using AmazingBeer.Api.Application.Dtos.Cerveja;
using AmazingBeer.Api.Application.Interfaces;
using AmazingBeer.Api.Application.Responses;
using AmazingBeer.Api.Domain.Exceptions;
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
                // Busca os dados no repositório:
                var cervejasResponse = await _cervejaRepository.RetornarCervejasRepositorioAsync();

                // Verifica se a operação foi bem-sucedida e se há dados:
                if (!cervejasResponse.Success || cervejasResponse.Data is null || !cervejasResponse.Data.Any())
                {
                    Log.Warning("NAO foram encontradas cervejas cadastradas no banco de dados.");
                    throw new CustomExceptions.NotFoundException("NÃO foram encontradas cervejas cadastradas no banco de dados.");
                }

                // Mapeia os dados para o Dto:
                var cervejas = _mapper.Map<List<ListarCervejaDto>>(cervejasResponse.Data);

                /// Retorna as cervejas para a Controller:
                Log.Information($"Cervejas retornadas com sucesso. Total: {cervejas.Count}");
                return new ResponseBase<IEnumerable<ListarCervejaDto>>(success: true, message: "Cervejas retornadas com sucesso.", data: cervejas);
            }
            catch (Exception ex)
            {
                throw new CustomExceptions.InternalServerErrorException(ex.Message);
            }
        }

        public async Task<ResponseBase<ListarCervejaDto>> RetornarCervejaIdAsync(Guid id)
        {
            try
            {
                // Busca a cerveja pelo Id no repositório:
                var cervejaIdResponse = await _cervejaRepository.RetornarCervejasIdRepositorioAsync(id);

                // Verifica se a operação foi bem-sucedida e se há dados:
                if (!cervejaIdResponse.Success || cervejaIdResponse.Data is null)
                {
                    Log.Warning($"Cerveja com Id: '{id}' NAO encontrada na base de dados.");
                    throw new CustomExceptions.NotFoundException($"Cerveja com Id: '{id}' NÃO encontrada na base de dados.");
                }

                // Mapeia os dados para o Dto:
                var cervejaId = _mapper.Map<ListarCervejaDto>(cervejaIdResponse.Data);

                // Retorna a cerveja para a Controller:
                Log.Information($"Cerveja com Id: '{id}' retornada com sucesso.");
                return new ResponseBase<ListarCervejaDto>(success: true, message: "Cerveja retornada com sucesso.", data: cervejaId);
            }
            catch (Exception ex)
            {
                throw new CustomExceptions.InternalServerErrorException(ex.Message);
            }
        }

        public async Task<ResponseBase<ListarCervejaDto>> AdicionarCervejaAsync(CriarCervejaDto cervejaCriarDto)
        {
            try
            {
                // Chamada ao repositório para adicionar a cerveja:
                var adicionarResponse = await _cervejaRepository.AdicionarCervejaRepositorioAsync(cervejaCriarDto);

                // Verifica o retorno do repositório:
                if (!adicionarResponse.Success)
                {
                    Log.Warning("Erro no retorno do repositório.");
                    throw new CustomExceptions.ValidationException("Erro no retorno do repositório.");
                }

                // Recupera a cerveja adicionada:
                var cervejaAdicionada = adicionarResponse.Data?.FirstOrDefault();

                // Verifica se a cerveja foi adicionada:
                if (cervejaAdicionada is null)
                {
                    Log.Warning("Falha ao recuperar a cerveja recém-cadastrada.");
                    throw new CustomExceptions.ValidationException("Falha ao recuperar a cerveja recém-cadastrada.");
                }

                // Retorna a cerveja para a Controller:
                Log.Information("Cerveja adicionada com sucesso.");
                return new ResponseBase<ListarCervejaDto>(success: true, message: "Cerveja adicionada com sucesso.", data: cervejaAdicionada);
            }
            catch (Exception ex)
            {
                throw new CustomExceptions.InternalServerErrorException(ex.Message);
            }
        }

        public async Task<ResponseBase<ListarCervejaDto>> EditarCervejaAsync(EditarCervejaDto cervejaEditarDto)
        {
            // Validação inicial dos dados recebidos:
            if (cervejaEditarDto is null)
            {
                Log.Warning("SERVICE: Os dados da cerveja nao podem ser nulos.");
                return new ResponseBase<ListarCervejaDto>(success: false, message: "OS dados da cerveja não podem ser nulos.", data: null);
            }

            try
            {
                // Chamada ao repositório para editar a cerveja:
                var editarResponse = await _cervejaRepository.EditarCervejaRepositorioAsync(cervejaEditarDto);

                // Verifica o retorno do repositório:
                if (!editarResponse.Success)
                {
                    Log.Warning("SERVICE: Erro no retorno do repositório.");
                    return new ResponseBase<ListarCervejaDto>(success: false, message: editarResponse.Message, data: null);
                }

                // Recupera a cerveja editada:
                var cervejaEditada = editarResponse.Data?.FirstOrDefault();

                // Verifica se a cerveja foi editada:
                if(cervejaEditada is null)
                {
                    Log.Warning("SERVICE: Falha ao recuperar a cerveja recém-editada.");
                    return new ResponseBase<ListarCervejaDto>(success: false, message: "Falha ao recuperar a cerveja recém-editada.", data: null);
                }

                // Retorna a cerveja para a Controller:
                Log.Information("SERVICE: Cerveja editada com sucesso.");
                return new ResponseBase<ListarCervejaDto>(success: true, message: "Cerveja editada com sucesso.", data: cervejaEditada);
            }
            catch (Exception ex)
            {
                // Loga o erro com detalhes e retorna uma mensagem genérica:
                Log.Error($"SERVICE: Erro ao editar cerveja: {ex.Message}", ex);
                return new ResponseBase<ListarCervejaDto>(success: false, message: "Erro inesperado ao editar a cerveja.", data: null);
            }
        }

        public async Task<ResponseBase<ListarCervejaDto>> DeletarCervejaAsync(Guid Id)
        {
            try
            {
                // Chamada ao repositório para deletar a cerveja:
                var deletarResponse = await _cervejaRepository.DeletarCervejaRepositorioAsync(Id);

                // Verifica o retorno do repositório:
                if (!deletarResponse.Success)
                {
                    Log.Warning("SERVICE: Erro no retorno do repositório.");
                    return new ResponseBase<ListarCervejaDto>(success: false, message: deletarResponse.Message, data: null);
                }

                // Retorna a cerveja para a Controller:
                Log.Information("SERVICE: Cerveja deletada com sucesso.");
                return new ResponseBase<ListarCervejaDto>(success: true, message: "Cerveja deletada com sucesso.", data: null);
            }
            catch (Exception ex)
            {
                // Loga o erro com detalhes e retorna uma mensagem genérica:
                Log.Error($"SERVICE: Erro ao deletar cerveja: {ex.Message}", ex);
                return new ResponseBase<ListarCervejaDto>(success: false, message: "Erro inesperado ao deletar a cerveja.", data: null);
            }
        }
    }
}
