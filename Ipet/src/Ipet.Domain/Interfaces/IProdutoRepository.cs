using Ipet.Domain.Models;

namespace Ipet.Domain.Intefaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<List<Produto>> GetProdutosByTag(string[] tags, List<Produto> todosProdutos);
        Task<List<Produto>> ObterProdutosOrdenadosPorAvaliacao();
    }
}