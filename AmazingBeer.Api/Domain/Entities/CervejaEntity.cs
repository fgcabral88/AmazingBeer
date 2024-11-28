namespace AmazingBeer.Api.Domain.Entitys
{
    public class CervejaEntity : BaseEntity
    {
        public string Nome { get; set; } // Nome da Cerveja
        public string Estilo { get; set; } // Estilo da Cerveja Ex: IPA, Lager, Stout
        public double TeorAlcoolico { get; set; } // Teor Alcoolico da Cerveja em porcentagem
        public string Descricao { get; set; } // Descrição da Cerveja
        public decimal Preco { get; set; } // Preço da Cerveja por unidade
        public int VolumeML { get; set; } // Volume em mililitros
        public Guid FabricanteId { get; set; } // Id do Fabricante da Cerveja Ex: Guid
        public FabricanteEntity Fabricante { get; set; } // Informações do Fabricante da Cerveja
        public List<CervejaIngredienteEntity> Ingredientes { get; set; } // Ingredientes da Cerveja
        public UsuarioEntity Usuario { get; set; } // Usuário criador ou responsável pela Cerveja
    }
}
