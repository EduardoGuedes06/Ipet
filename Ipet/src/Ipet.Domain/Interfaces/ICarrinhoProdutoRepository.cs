using Ipet.Domain.Models;

namespace Ipet.Domain.Intefaces
{
    public interface ICarrinhoProdutoRepository : IRepository<CarrinhoProduto>
    {
        Task<Guid> ObterCarrinhoIdPorUsuarioId(Guid usuarioId);
        Task<bool> ProdutoExisteEmCarrinho(Guid produtoId);
        Task<bool> RemoverprodutoPorCarrinho(Guid carrinhoId, Guid produtoId);
        Task<bool> RemoverProdutosDoCarrinho(Guid produtoId);
    }
}