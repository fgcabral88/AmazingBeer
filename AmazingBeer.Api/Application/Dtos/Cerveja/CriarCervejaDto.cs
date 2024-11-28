using AmazingBeer.Api.Domain.Entitys;

namespace AmazingBeer.Api.Application.Dtos.Cerveja
{
    public class CriarCervejaDto
    {
        public string Nome { get; set; }
        public string Estilo { get; set; } 
        public double TeorAlcoolico { get; set; } 
        public string Descricao { get; set; }
        public decimal Preco { get; set; } 
        public int VolumeML { get; set; } 
        public Guid FabricanteId { get; set; }
        public FabricanteEntity Fabricante { get; set; }
        public List<CervejaIngredienteEntity> Ingredientes { get; set; }
        public UsuarioEntity Usuario { get; set; } 
    }
}
