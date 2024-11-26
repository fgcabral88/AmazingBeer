using AmazingBeer.Api.Domain.Entitys;

namespace AmazingBeer.Api.Application.Dtos.Cerveja
{
    public class EditarCervejaDto : BaseDto
    {
        public string Nome { get; set; }
        public string Estilo { get; set; } // Ex: IPA, Lager, Stout
        public double TeorAlcoolico { get; set; } // Em porcentagem
        public string Descricao { get; set; }
        public decimal Preco { get; set; } // Preço por unidade
        public int VolumeML { get; set; } // Volume em mililitros
        public Guid FabricanteId { get; set; }
        public FabricanteEntity Fabricante { get; set; }
        public List<CervejaIngredienteEntity> Ingredientes { get; set; }
        public UsuarioEntity Usuario { get; set; } // Criador ou responsável pela cerveja
    }
}
