using AmazingBeer.Api.Domain.Entitys;
using System.Text.Json.Serialization;

namespace AmazingBeer.Api.Application.Dtos.Cerveja
{
    public class ListarCervejaDto : BaseDto
    {
        [JsonPropertyOrder(2)]
        public string Nome { get; set; }
        [JsonPropertyOrder(4)]
        public string Estilo { get; set; }
        [JsonPropertyOrder(5)]
        public double TeorAlcoolico { get; set; }
        [JsonPropertyOrder(3)]
        public string Descricao { get; set; }
        [JsonPropertyOrder(6)]
        public decimal Preco { get; set; }
        [JsonPropertyOrder(7)]
        public int VolumeML { get; set; }
        [JsonPropertyOrder(9)]
        public Guid FabricanteId { get; set; }
        [JsonPropertyOrder(10)]
        public FabricanteEntity Fabricante { get; set; }
        [JsonPropertyOrder(8)]
        public List<CervejaIngredienteEntity> Ingredientes { get; set; }
        [JsonPropertyOrder(11)]
        public UsuarioEntity Usuario { get; set; } 
    }
}
