namespace AmazingBeer.Api.Domain.Entitys
{
    public class CervejaIngredienteEntity
    {
        public Guid CervejaId { get; set; }
        public CervejaEntity Cerveja { get; set; }
        public Guid IngredienteId { get; set; }
        public IngredienteEntity Ingrediente { get; set; }
        public double Quantidade { get; set; } // Em gramas ou mililitros
        public string Unidade { get; set; } // Ex: g, ml
    }
}
