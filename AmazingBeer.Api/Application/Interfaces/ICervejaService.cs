﻿using AmazingBeer.Api.Application.Dtos.Cerveja;
using AmazingBeer.Api.Application.Responses;

namespace AmazingBeer.Api.Application.Interfaces
{
    public interface ICervejaService
    {
        Task<ResponseBase<ListarCervejaDto>> RetornarCervejasAsync();

        Task<ResponseBase<ListarCervejaDto>> RetornarCervejaIdAsync(int Id);

        Task<ResponseBase<ListarCervejaDto>> AdicionarCervejaAsync(CriarCervejaDto cervejaCriarDto);

        Task<ResponseBase<ListarCervejaDto>> EditarCervejaAsync(EditarCervejaDto cervejaEditarDto);

        Task<ResponseBase<ListarCervejaDto>> DeletarCervejaAsync(int Id);
    }
}