namespace AmazingBeer.Api.Domain.Entitys
{
    public class CervejaEntity : BaseEntity
    {
        public required string Nome { get; set; } // Nome da Cerveja [Obrigatório]
        public required string Estilo { get; set; } // Estilo da Cerveja Ex: IPA, Lager, Stout [Obrigatório]
        public required double TeorAlcoolico { get; set; } // Teor Alcoolico da Cerveja em porcentagem [Obrigatório]
        public string? Descricao { get; set; } // Descrição da Cerveja [NÃO Obrigatório]
        public required decimal Preco { get; set; } // Preço da Cerveja por unidade [Obrigatório]
        public required int VolumeML { get; set; } // Volume em mililitros [Obrigatório]
        public required Guid FabricanteId { get; set; } // Id do Fabricante da Cerveja Ex: Guid [Obrigatório]
        public required Guid UsuarioId { get; set; } // Id do Usuário criador ou responsável pela Cerveja [Obrigatório]
    } 
}
