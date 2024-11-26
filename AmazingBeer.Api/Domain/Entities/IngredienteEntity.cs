namespace AmazingBeer.Api.Domain.Entitys
{
    public class IngredienteEntity : BaseEntity
    {
        public string Nome { get; set; } // Ex: Lúpulo, Malte, Água
        public string Tipo { get; set; } // Ex: Malteado, Não-Malteado
        public string Descricao { get; set; }
        public List<CervejaIngredienteEntity> Cervejas { get; set; }
    }
}
