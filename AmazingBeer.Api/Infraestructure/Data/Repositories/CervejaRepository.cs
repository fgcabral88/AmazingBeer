﻿using AmazingBeer.Api.Application.Dtos.Cerveja;
using AmazingBeer.Api.Application.Responses;
using AmazingBeer.Api.Domain.Exceptions;
using AmazingBeer.Api.Domain.Interfaces;
using AmazingBeer.Api.Infraestructure.Data.Context;
using Dapper;
using Microsoft.Data.SqlClient;
using Serilog;

namespace AmazingBeer.Api.Infraestructure.Data.Repositories
{
    public class CervejaRepository : ICervejaRepository, IDisposable
    {
        private readonly SqlDbContext _dbContext;
        private bool _disposed = false;

        public CervejaRepository(SqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseBase<IEnumerable<ListarCervejaDto>>> RetornarCervejasRepositorioAsync()
        {
            try
            {
                // Consulta todas as cervejas:
                const string query = "SELECT * FROM Cervejas";

                // Abre a conexão com o banco de dados:
                using var conexao = _dbContext.CreateConnection();
                conexao.Open();

                // Recupera todas as cervejas no banco de dados:
                var cervejas = await conexao.QueryAsync<ListarCervejaDto>(query);

                // Valida se as cervejas foram encontradas:
                if (!cervejas.Any())
                {
                    return new ResponseBase<IEnumerable<ListarCervejaDto>>(success: false, message: "NÃO foram encontradas cervejas cadastradas no banco de dados.", data: null);
                }

                // Retorna as cervejas recuperadas para a Service:
                return new ResponseBase<IEnumerable<ListarCervejaDto>>(success: true, message: "Cervejas recuperadas com sucesso do banco de dados.", data: cervejas);
            }
            catch (SqlException ex)
            {
                Log.Error(ex.Message, ex);
                throw new CustomExceptions.DatabaseException(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw new CustomExceptions.InternalServerErrorException(ex.Message);
            }
        }

        public async Task<ResponseBase<ListarCervejaDto>> RetornarCervejasIdRepositorioAsync(Guid id)
        {
            // Consulta a cerveja pelo Id:
            const string query = "SELECT * FROM Cervejas WHERE Id = @Id";

            try
            {
                // Abre a conexão com o banco de dados:
                using var conexao = _dbContext.CreateConnection();
                conexao.Open();

                // Recupera a cerveja no banco de dados:
                var cerveja = await conexao.QueryFirstOrDefaultAsync<ListarCervejaDto>(query, new { Id = id });

                // Valida se a cerveja foi encontrada:
                if (cerveja is null)
                {
                    return new ResponseBase<ListarCervejaDto>(success: false, message: $"Cerveja com Id {id} NÃO encontrada no banco de dados.", data: null);
                }

                // Retorna a cerveja recuperada para a Service:
                return new ResponseBase<ListarCervejaDto>(success: true, message: "Cerveja recuperada com sucesso.", data: cerveja);
            }
            catch (SqlException ex)
            {
                Log.Error(ex.Message, ex);
                throw new CustomExceptions.DatabaseException(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw new CustomExceptions.InternalServerErrorException(ex.Message);
            }
        }

        public async Task<ResponseBase<List<ListarCervejaDto>>> AdicionarCervejaRepositorioAsync(CriarCervejaDto criarCervejaDto)
        {
            // Cadastra a cerveja no banco de dados:
            const string query = @"INSERT INTO Cervejas (Nome, Estilo, TeorAlcoolico, Descricao, Preco, VolumeML, FabricanteId, UsuarioId) OUTPUT INSERTED.Id
                 VALUES (@Nome, @Estilo, @TeorAlcoolico, @Descricao, @Preco, @VolumeML, @FabricanteId, @UsuarioId);";

            try
            {
                // Abre conexão com o banco de dados:
                using var conexao = _dbContext.CreateConnection();
                conexao.Open();

                // Consulta para verificar se a cerveja já existe no banco de dados:
                const string queryVerificacao = @"SELECT COUNT(1) FROM Cervejas WHERE Nome = @Nome AND Estilo = @Estilo AND FabricanteId = @FabricanteId";

                var existeCerveja = await conexao.ExecuteScalarAsync<int>(queryVerificacao, new { criarCervejaDto.Nome, criarCervejaDto.Estilo, criarCervejaDto.FabricanteId });

                if(existeCerveja > 0)
                {
                    Log.Warning("Cerveja ja existe no banco de dados.");
                    throw new CustomExceptions.ValidationException("Cerveja ja existe no banco de dados.");
                }

                // Inicia uma transação:
                using var transacao = conexao.BeginTransaction();

                // Retorna o Id recém-inserido:
                var cervejaId = await conexao.ExecuteScalarAsync<Guid>(query, criarCervejaDto, transaction: transacao);

                // Valida se a cerveja foi adicionada ao banco de dados:
                if (cervejaId == Guid.Empty)
                {
                    transacao.Rollback();

                    Log.Warning("Nenhuma cerveja foi adicionada ao banco de dados.");
                    throw new CustomExceptions.ValidationException("Nenhuma cerveja foi adicionada ao banco de dados.");
                }

                // Consulta a cerveja recém-inserida:
                const string querySelect = @"SELECT Id, Nome, Estilo, TeorAlcoolico, Descricao, Preco, VolumeML, FabricanteId, UsuarioId FROM Cervejas WHERE Id = @Id";

                // Recupera a cerveja no banco de dados:
                var cervejas = (await conexao.QueryAsync<ListarCervejaDto>(querySelect, new { Id = cervejaId }, transaction: transacao)).ToList();

                transacao.Commit();

                // Retorna a cerveja recuperada para a Service:
                return new ResponseBase<List<ListarCervejaDto>>(success: true, message: "Cerveja cadastrada com sucesso.", data: cervejas);
            }
            catch (SqlException ex)
            {
                Log.Error(ex.Message, ex);
                throw new CustomExceptions.DatabaseException(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw new CustomExceptions.InternalServerErrorException(ex.Message);
            }
        }

        public async Task<ResponseBase<List<ListarCervejaDto>>> EditarCervejaRepositorioAsync(EditarCervejaDto editarCervejaDto)
        {
            try
            {
                // Query para atualizar a cerveja
                const string query = @"UPDATE Cervejas SET Nome = @Nome, Estilo = @Estilo, TeorAlcoolico = @TeorAlcoolico, Descricao = @Descricao, Preco = @Preco, 
                                   VolumeML = @VolumeML, FabricanteId = @FabricanteId, UsuarioId = @UsuarioId WHERE Id = @Id; SELECT * FROM Cervejas WHERE Id = @Id;";

                // Abre conexão com o banco de dados
                using var conexao = _dbContext.CreateConnection();
                conexao.Open();

                // Inicia uma transação
                using var transacao = conexao.BeginTransaction();

                // Executa a query de atualização e retorna a cerveja editada
                var cervejas = await conexao.QueryAsync<ListarCervejaDto>(query, editarCervejaDto, transaction: transacao);

                // Verifica se a cerveja foi encontrada
                if (!cervejas.Any())
                {
                    transacao.Rollback();

                    Log.Warning("Nenhuma cerveja foi editada.");
                    throw new CustomExceptions.ValidationException("Nenhuma cerveja foi editada.");
                }

                // Confirma a transação
                transacao.Commit();

                Log.Information("Cerveja editada com sucesso.");
                return new ResponseBase<List<ListarCervejaDto>>(success: true, message: "Cerveja editada com sucesso.", data: cervejas.ToList());
            }
            catch (SqlException ex)
            {
                Log.Error(ex.Message, ex);
                throw new CustomExceptions.DatabaseException(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw new CustomExceptions.InternalServerErrorException(ex.Message);
            }
        }

        public async Task<ResponseBase<List<ListarCervejaDto>>> DeletarCervejaRepositorioAsync(Guid id)
        {
            try
            {
                // Query para recuperar os dados da cerveja antes de deletar:
                const string querySelect = @"SELECT Id, Nome, Estilo, TeorAlcoolico, Descricao, Preco, VolumeML, FabricanteId, UsuarioId FROM Cervejas WHERE Id = @Id";

                // Query para deletar a cerveja:
                const string queryDelete = "DELETE FROM Cervejas WHERE Id = @Id;";

                // Abre uma conexão com o banco de dados:
                using var conexao = _dbContext.CreateConnection();
                conexao.Open();

                // Inicia uma transação:
                using var transacao = conexao.BeginTransaction();

                // Recupera os dados da cerveja:
                var cerveja = await conexao.QueryFirstOrDefaultAsync<ListarCervejaDto>(querySelect, new { Id = id }, transaction: transacao);

                // Verifica se a cerveja foi encontrada:
                if (cerveja == null)
                {
                    transacao.Rollback();

                    Log.Warning("Cerveja não encontrada.");
                    throw new CustomExceptions.ValidationException("Cerveja não encontrada.");
                }

                // Executa a query de exclusão:
                var linhasAfetadas = await conexao.ExecuteAsync(queryDelete, new { Id = id }, transaction: transacao);

                // Valida se a exclusão foi realizada:
                if (linhasAfetadas == 0)
                {
                    transacao.Rollback();

                    Log.Warning("Nenhuma cerveja foi deletada.");
                    throw new CustomExceptions.ValidationException("Nenhuma cerveja foi deletada.");
                }

                // Confirma a transação:
                transacao.Commit();

                // Retorna a cerveja deletada para a Service:
                Log.Information("Cerveja deletada com sucesso.");
                return new ResponseBase<List<ListarCervejaDto>>(success: true, message: "Cerveja deletada com sucesso.", data: new List<ListarCervejaDto> { cerveja });
            }
            catch (SqlException ex)
            {
                Log.Error(ex.Message, ex);
                throw new CustomExceptions.DatabaseException(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw new CustomExceptions.InternalServerErrorException(ex.Message);
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                if (_dbContext != null)
                {
                    _dbContext.CreateConnection().Dispose();
                }

                _disposed = true;
            }
        }
    }
}
