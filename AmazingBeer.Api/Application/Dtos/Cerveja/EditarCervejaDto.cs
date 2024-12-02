using System.Text.Json.Serialization;

namespace AmazingBeer.Api.Application.Dtos.Cerveja
{
    public class EditarCervejaDto : BaseDto
    {
        [JsonRequired]
        [JsonPropertyName("Nome da Cerveja")]
        public required string Nome { get; set; }

        [JsonRequired]
        [JsonPropertyName("Estilo da Cerveja")]
        public required string Estilo { get; set; }

        [JsonRequired]
        [JsonPropertyName("Teor Alcoólico")]
        public required double TeorAlcoolico { get; set; }

        [JsonPropertyName("Descrição da Cerveja")]
        public string? Descricao { get; set; }

        [JsonRequired]
        [JsonPropertyName("Preço da Cerveja")]
        public required decimal Preco { get; set; }

        [JsonRequired]
        [JsonPropertyName("Volume em Mililitros")]
        public required int VolumeML { get; set; }

        [JsonRequired]
        [JsonPropertyName("Id do Fabricante")]
        public required Guid FabricanteId { get; set; }

        [JsonRequired]
        [JsonPropertyName("Id do Usuário")]
        public required Guid UsuarioId { get; set; } 
    }
}
