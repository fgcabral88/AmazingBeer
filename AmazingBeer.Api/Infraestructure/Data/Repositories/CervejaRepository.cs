using AmazingBeer.Api.Application.Dtos.Cerveja;
using AmazingBeer.Api.Application.Responses;
using AmazingBeer.Api.Domain.Interfaces;
using Dapper;
using System.Data;

namespace AmazingBeer.Api.Infraestructure.Data.Repositories
{
    public class CervejaRepository : ICervejaRepository, IDisposable
    {
        private readonly IDbConnection _dbConnection;
        private bool _disposed = false;

        public CervejaRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<ResponseBase<IEnumerable<ListarCervejaDto>>> RetornarCervejasRepositorioAsync()
        {
            try
            {
                using (var conexao = _dbConnection)
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
                if (_dbConnection != null)
                {
                    _dbConnection.Dispose();
                }

                _disposed = true;
            }
        }
    }
}
