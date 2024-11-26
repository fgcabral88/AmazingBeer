namespace AmazingBeer.Api.Domain.Entitys
{
    public class FabricanteEntity : BaseEntity
    {
        public string Nome { get; set; }
        public string PaisOrigem { get; set; }
        public string Descricao { get; set; }
        public List<CervejaEntity> Cervejas { get; set; }
    }
}
