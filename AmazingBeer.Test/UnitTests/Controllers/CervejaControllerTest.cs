using AmazingBeer.Api.Application.Dtos.Cerveja;
using AmazingBeer.Api.Application.Interfaces;
using AmazingBeer.Api.Application.Responses;
using AmazingBeer.Api.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using static AmazingBeer.Api.Domain.Exceptions.CustomExceptions;

namespace AmazingBeer.Tests.UnitTests.Controllers
{
    public class CervejaControllerTest
    {
        private readonly CervejaController _cervejaController;
        private readonly Mock<ICervejaService> _cervejaServiceMock;

        public CervejaControllerTest() 
        {
            _cervejaServiceMock = new Mock<ICervejaService>();
            _cervejaController = new CervejaController(_cervejaServiceMock.Object);
        }

        [Fact]
        public async Task RetornarCervejasAsync_Sucesso_RetornaResultadoOk()
        {
            // Arrange
            var cervejas = new List<ListarCervejaDto>
            {
                new ListarCervejaDto
                {
                    Id = Guid.NewGuid(),
                    Nome = "Cerveja Teste",
                    Descricao = "Descrição Cerveja Teste",
                    Estilo = "Estilo Cerveja Teste",
                    TeorAlcoolico = 10,
                    Preco = 10,
                    VolumeML = 450,
                    FabricanteId = Guid.NewGuid(),
                    UsuarioId = Guid.NewGuid()
                }
            };

            var resposta = new ResponseBase<IEnumerable<ListarCervejaDto>>(cervejas, true, "Sucesso");
            _cervejaServiceMock.Setup(s => s.RetornarCervejasAsync())
                .ReturnsAsync(resposta);

            // Act
            var resultado = await _cervejaController.RetornarCervejasAsync();

            // Assert
            var resultadoOk = Assert.IsType<OkObjectResult>(resultado);

            Assert.Equal(200, resultadoOk.StatusCode);

            var retornarResultado = Assert.IsType<ResponseBase<IEnumerable<ListarCervejaDto>>>(resultadoOk.Value);

            Assert.True(retornarResultado.Success);
            Assert.Equal(resposta.Data, retornarResultado.Data);
            Assert.Equal(resposta.Message, retornarResultado.Message);
        }

        [Fact]
        public async Task RetornarCervejasAsync_NaoEncontrado_LancaNotFoundException()
        {
            // Arrange
            var response = new ResponseBase<IEnumerable<ListarCervejaDto>>(null, false, "Nenhuma cerveja encontrada");
            
            _cervejaServiceMock.Setup(s => s.RetornarCervejasAsync())
                .ReturnsAsync(response);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _cervejaController.RetornarCervejasAsync());
            Assert.Equal("Nenhuma cerveja encontrada", exception.Message);
        }

        [Fact]
        public async Task RetornarCervejaPorIdAsync_Sucesso_RetornaResultadoOk()
        {
            // Arrange
            var id = Guid.NewGuid();
            var cerveja = new ListarCervejaDto {
                Id = id,
                Nome = "Cerveja Teste",
                Descricao = "Descrição Cerveja Teste",
                Estilo = "Estilo Cerveja Teste",
                TeorAlcoolico = 10,
                Preco = 10,
                VolumeML = 450,
                FabricanteId = Guid.NewGuid(),
                UsuarioId = Guid.NewGuid()
            };

            var resposta = new ResponseBase<ListarCervejaDto>(cerveja, true, "Sucesso");

            _cervejaServiceMock.Setup(s => s.RetornarCervejaIdAsync(id))
                               .ReturnsAsync(resposta);

            // Act
            var resultado = await _cervejaController.RetornarCervejaPorIdAsync(id);

            // Assert
            var resultadoOk = Assert.IsType<OkObjectResult>(resultado);

            Assert.Equal(200, resultadoOk.StatusCode);
            Assert.Equal(resposta, resultadoOk.Value);
        }

        [Fact]
        public async Task RetornarCervejaPorIdAsync_IdInvalido_LancaBadRequestException()
        {
            // Arrange
            var idInvalido = Guid.Empty;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _cervejaController.RetornarCervejaPorIdAsync(idInvalido));
            Assert.Equal("O Id informado é inválido.", exception.Message);
        }

        [Fact]
        public async Task AdicionarCervejaAsync_Sucesso_RetornaResultadoOk()
        {
            // Arrange
            var cervejaNova = new CriarCervejaDto 
            { 
                Nome = "Cerveja Nova", 
                Descricao = "Descrição Cerveja Nova",
                Estilo = "Estilo Cerveja Nova",
                TeorAlcoolico = 10,
                Preco = 10,
                VolumeML = 450,
                FabricanteId = Guid.NewGuid(),
                UsuarioId = Guid.NewGuid()
            };

            var cerveja = new ListarCervejaDto
            {
                Id = Guid.NewGuid(),
                Nome = "Cerveja Teste",
                Descricao = "Descrição Cerveja Teste",
                Estilo = "Estilo Cerveja Teste",
                TeorAlcoolico = 10,
                Preco = 10,
                VolumeML = 450,
                FabricanteId = Guid.NewGuid(),
                UsuarioId = Guid.NewGuid()
            };

            var resposta = new ResponseBase<ListarCervejaDto>(cerveja, true, "Cerveja cadastrada com sucesso");

            _cervejaServiceMock.Setup(s => s.AdicionarCervejaAsync(cervejaNova))
                               .ReturnsAsync(resposta);

            // Act
            var resultado = await _cervejaController.AdicionarCervejaAsync(cervejaNova);

            // Assert
            var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
            Assert.Equal(200, resultadoOk.StatusCode);
            Assert.Equal(resposta, resultadoOk.Value);
        }
    }
}