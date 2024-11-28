using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AmazingBeer.Api.Domain
{
    public class BaseEntity
    {
        [JsonRequired]
        [JsonPropertyName("Identificador da Cerveja")]
        public required Guid Id { get; set; }
    }
}
