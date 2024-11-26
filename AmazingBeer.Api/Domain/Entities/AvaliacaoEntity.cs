namespace AmazingBeer.Api.Domain.Entitys
{
    public class AvaliacaoEntity : BaseEntity
    {
        public Guid UsuarioId { get; set; }
        public UsuarioEntity Usuario { get; set; } // Relacionamento com o usuário que fez a avaliação
        public Guid CervejaId { get; set; }
        public CervejaEntity Cerveja { get; set; }
        public int Nota { get; set; } // Escala de 1 a 5
        public string Comentario { get; set; }
        public DateTime Data { get; set; }
    }
}
