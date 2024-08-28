namespace Ipet.Domain.Models
{
    public class CarrinhoProduto : Entity
    {
        public Guid CarrinhoId { get; set; }
        public Carrinho Carrinho { get; set; }
        public Guid ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
    }
}
