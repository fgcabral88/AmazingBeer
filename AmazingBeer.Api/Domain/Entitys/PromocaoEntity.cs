namespace AmazingBeer.Api.Domain.Entitys
{
    public class PromocaoEntity : BaseEntity
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public decimal DescontoPercentual { get; set; } // Ex: 15% de desconto
        public List<CervejaEntity> Cervejas { get; set; }
        public UsuarioEntity Usuario { get; set; } // Criador da promoção
    }
}
