using System.Text.Json.Serialization;

namespace AmazingBeer.Api.Application.Dtos
{
    public class BaseDto
    {
        [JsonRequired]
        [JsonPropertyOrder(1)]
        [JsonPropertyName("Identificador da Cerveja")]
        public required Guid Id { get; set; }
    }
}
