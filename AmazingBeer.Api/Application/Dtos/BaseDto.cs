using System.Text.Json.Serialization;

namespace AmazingBeer.Api.Application.Dtos
{
    public class BaseDto
    {
        [JsonPropertyOrder(1)]
        public Guid Id { get; set; }
    }
}
