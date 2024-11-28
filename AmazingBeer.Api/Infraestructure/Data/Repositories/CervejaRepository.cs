using AmazingBeer.Api.Application.Dtos.Cerveja;
using AmazingBeer.Api.Application.Responses;
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
                    Log.Warning("REPOSITORIO: NAO foram encontradas cervejas cadastradas no banco de dados.");
                    return new ResponseBase<IEnumerable<ListarCervejaDto>>(success: false, message: "NÃO foram encontradas cervejas cadastradas no banco de dados.", data: null);
                }

                // Retorna as cervejas recuperadas para a Service:
                Log.Information($"REPOSITORIO: Cervejas recuperadas com sucesso. Total: {cervejas.Count()}");
                return new ResponseBase<IEnumerable<ListarCervejaDto>>(success: true, message: "Cervejas recuperadas com sucesso do banco de dados.", data: cervejas);
            }
            catch (SqlException ex)
            {
                // Erro ao acessar o banco de dados:
                Log.Error($"REPOSITORIO: Erro ao acessar o banco de dados. Detalhes: {ex.Message}", ex);
                return new ResponseBase<IEnumerable<ListarCervejaDto>>(success: false, message: "Erro ao acessar o banco de dados. Tente novamente mais tarde.", data: null);
            }
            catch (Exception ex)
            {
                // Erro inesperado:
                Log.Error($"REPOSITORIO: Erro ao recuperar cervejas do banco de dados: {ex.Message}", ex);
                return new ResponseBase<IEnumerable<ListarCervejaDto>>(success: false, message: "Ocorreu um erro ao recuperar as cervejas do banco de dados.", data: null);
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
                    Log.Warning($"REPOSITORIO: Cerveja com Id {id} NAO encontrada no banco de dados.");
                    return new ResponseBase<ListarCervejaDto>(success: false, message: $"Cerveja com Id {id} NÃO encontrada no banco de dados.", data: null);
                }

                // Retorna a cerveja recuperada para a Service:
                Log.Information($"REPOSITORIO: Cerveja com Id {id} recuperada com sucesso do banco de dados.");
                return new ResponseBase<ListarCervejaDto>(success: true, message: "Cerveja recuperada com sucesso.", data: cerveja);
            }
            catch (SqlException ex)
            {
                // Erro ao acessar o banco de dados:
                Log.Error($"REPOSITORIO: Erro ao acessar o banco de dados para o Id {id}. Detalhes: {ex.Message}", ex);
                return new ResponseBase<ListarCervejaDto>(success: false, message: "Erro ao acessar o banco de dados. Tente novamente mais tarde.", data: null);
            }
            catch (Exception ex)
            {
                // Erro inesperado:
                Log.Error($"REPOSITORIO: Erro inesperado ao recuperar cerveja com Id {id}. Detalhes: {ex.Message}", ex);
                return new ResponseBase<ListarCervejaDto>(success: false, message: "Ocorreu um erro inesperado. Tente novamente mais tarde.", data: null);
            }
        }

        public async Task<ResponseBase<List<ListarCervejaDto>>> AdicionarCervejaRepositorioAsync(CriarCervejaDto criarCervejaDto)
        {            
            const string query = @"INSERT INTO Cervejas (Id, Nome, Estilo, TeorAlcoolico, Descricao, Preco, VolumeML, FabricanteId, UsuarioId) 
               VALUES (@Id, @Nome, @Estilo, @TeorAlcoolico, @Descricao, @Preco, @VolumeML, @FabricanteId, @UsuarioId)";

            try
            {
                using var conexao = _dbContext.CreateConnection();
                conexao.Open();

                using var transacao = conexao.BeginTransaction();
                var cervejaAdicionar = await conexao.ExecuteScalarAsync<int>(query, criarCervejaDto, transaction: transacao);

                if (cervejaAdicionar == 0)
                {
                    transacao.Rollback();
                    return new ResponseBase<List<ListarCervejaDto>>(success: false, message: "Nenhuma cerveja foi adicionada", data: null);
                }

                // Consulta a cerveja recém-inserida
                const string querySelect = @"SELECT Id, Nome, Estilo, TeorAlcoolico, Descricao, Preco, VolumeML, FabricanteId, UsuarioId 
                                     FROM Cervejas WHERE Id = @Id";

                var cervejas = (await conexao.QueryAsync<ListarCervejaDto>(querySelect, new { Id = cervejaAdicionar }, transaction: transacao)).ToList();

                transacao.Commit();

                return new ResponseBase<List<ListarCervejaDto>>(success: true, message: "Cerveja cadastrada com sucesso.", data: cervejas);
            }
            catch (SqlException ex)
            {
                Log.Error($"Erro ao acessar o banco de dados: {ex.Message}", ex);
                return new ResponseBase<List<ListarCervejaDto>>(success: false, message: $"Erro ao acessar o banco de dados.", data: null);
            }
            catch (Exception ex)
            {
                Log.Error($"Erro inesperado: {ex.Message}", ex);
                return new ResponseBase<List<ListarCervejaDto>>(success: false, message: "Erro inesperado.", data: null);
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
