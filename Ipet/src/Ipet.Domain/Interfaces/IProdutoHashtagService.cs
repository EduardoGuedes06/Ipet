using Ipet.Domain.Models;

namespace Ipet.Domain.Intefaces
{
    public interface IProdutoHashtagService
    {
        Task RemoverProduto(Guid carrinhoId, Guid produtoId, int quantidade);
        Task FinalizarCompra(Guid carrinhoId);
        Task<Carrinho> ObterCarrinhoPorUsuario(Guid usuarioId);
        Task<Carrinho> CriarCarrinho(Guid usuarioId);
        Task RemoverCarrinho(Guid carrinhoId);
        Task AtualizarQuantidadeProduto(Guid carrinhoId, Guid produtoId, int novaQuantidade);
        Task<bool> AdicionarProduto(Guid userId, Guid produtoId, int quantidade);
    }
}