namespace AmazingBeer.Api.Domain.Entitys
{
    public class UsuarioEntity : BaseEntity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; } // Ou hash da senha
        public List<AvaliacaoEntity> Avaliacoes { get; set; }
        public List<PromocaoEntity> Promocoes { get; set; }
        public List<CervejaEntity> Cervejas { get; set; } // Se necessário
    }
}
