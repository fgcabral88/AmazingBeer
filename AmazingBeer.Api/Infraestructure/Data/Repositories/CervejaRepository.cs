using AmazingBeer.Api.Application.Dtos.Cerveja;
using AmazingBeer.Api.Application.Responses;
using AmazingBeer.Api.Domain.Interfaces;
using AmazingBeer.Api.Infraestructure.Data.Context;
using Dapper;
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
                using (var conexao = _dbContext.CreateConnection())
                {
                    if (conexao.State == ConnectionState.Closed)
                        conexao.Open();

                    const string query = "SELECT * FROM Cervejas";

                    var cervejas = await conexao.QueryAsync<ListarCervejaDto>(query);

                    return new ResponseBase<IEnumerable<ListarCervejaDto>>(success: true, message: "Cervejas recuperadas com sucesso do banco de dados.", data: cervejas);
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<IEnumerable<ListarCervejaDto>>(success: false, message: $"Erro em recuperar cervejas no banco de dados: {ex.Message}", data: null);
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
