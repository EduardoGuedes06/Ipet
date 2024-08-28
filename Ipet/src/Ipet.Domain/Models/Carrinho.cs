namespace Ipet.Domain.Models
{
    public class Carrinho : Entity
    {
        public Guid UsuarioId { get; set; }
        public ICollection<CarrinhoProduto> CarrinhoProdutos { get; set; }

    }
}
