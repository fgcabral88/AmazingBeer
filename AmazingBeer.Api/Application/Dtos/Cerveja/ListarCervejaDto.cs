using System.Text.Json.Serialization;

namespace AmazingBeer.Api.Application.Dtos.Cerveja
{
    public class ListarCervejaDto : BaseDto
    {
        [JsonPropertyOrder(2)]
        [JsonPropertyName("Nome da Cerveja")]
        public required string Nome { get; set; }

        [JsonPropertyOrder(4)]
        [JsonPropertyName("Estilo da Cerveja")]
        public required string Estilo { get; set; }

        [JsonPropertyOrder(5)]
        [JsonPropertyName("Teor Alcoólico")]
        public required double TeorAlcoolico { get; set; }

        [JsonPropertyOrder(3)]
        [JsonPropertyName("Descrição da Cerveja")]
        public string? Descricao { get; set; }

        [JsonPropertyOrder(6)]
        [JsonPropertyName("Preço da Cerveja")]
        public required decimal Preco { get; set; }

        [JsonPropertyOrder(7)]
        [JsonPropertyName("Volume em Mililitros")]
        public required int VolumeML { get; set; }

        [JsonPropertyOrder(9)]
        [JsonPropertyName("Id Fabricante")]
        public required Guid FabricanteId { get; set; }

        [JsonPropertyOrder(10)]
        [JsonPropertyName("Id Usuário")]
        public required Guid UsuarioId { get; set; }
    }
}
