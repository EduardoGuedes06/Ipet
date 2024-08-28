namespace Ipet.Domain.Models
{
    public class Favorito : Entity 
    {
        public Guid UsuarioId { get; set; }
        public Guid ProdutoId { get; set; }
    }
}
