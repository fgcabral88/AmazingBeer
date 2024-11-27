using AmazingBeer.Api.Application.Dtos.Cerveja;
using AmazingBeer.Api.Application.Responses;
using AmazingBeer.Api.Domain.Interfaces;
using AmazingBeer.Api.Infraestructure.Data.Context;
using Dapper;
using Serilog;
using System.Data;

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
                const string query = "SELECT * FROM Cervejas";

                using var conexao = _dbContext.CreateConnection();
                conexao.Open();

                var cervejas = await conexao.QueryAsync<ListarCervejaDto>(query);

                if (!cervejas.Any())
                {
                    Log.Warning("Nenhuma cerveja encontrada no banco de dados.");
                    return new ResponseBase<IEnumerable<ListarCervejaDto>>(success: false, message: "Nenhuma cerveja encontrada no banco de dados.", data: null);
                }

                Log.Information($"Cervejas recuperadas com sucesso. Total: {cervejas.Count()}");
                return new ResponseBase<IEnumerable<ListarCervejaDto>>(success: true, message: "Cervejas recuperadas com sucesso do banco de dados.", data: cervejas);
            }
            catch (Exception ex)
            {
                Log.Error($"Erro ao recuperar cervejas do banco de dados: {ex.Message}", ex);
                return new ResponseBase<IEnumerable<ListarCervejaDto>>(success: false, message: "Ocorreu um erro ao recuperar as cervejas do banco de dados.", data: null);
            }
        }

        public async Task<ResponseBase<ListarCervejaDto>> RetornarCervejasIdRepositorioAsync(Guid id)
        {
            try
            {
                using (var conexao = _dbContext.CreateConnection())
                {
                    if (conexao.State == ConnectionState.Closed)
                        conexao.Open();

                    const string query = "SELECT * FROM Cervejas WHERE Id = @Id";

                    var cervejaId = await conexao.QueryFirstOrDefaultAsync<ListarCervejaDto>(query, new { Id = id });

                    Log.Information($"Cerveja Id recuperada com sucesso do banco de dados.");
                    return new ResponseBase<ListarCervejaDto>(success: true, message: "Cerveja recuperada com sucesso do banco de dados.", data: cervejaId);
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Erro em recuperar cerveja no banco de dados. {ex.Message}");
                return new ResponseBase<ListarCervejaDto>(success: false, message: $"Erro em recuperar cerveja no banco de dados: {ex.Message}", data: null);
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
