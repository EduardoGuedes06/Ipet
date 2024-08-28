using Ipet.Domain.Models;

namespace Ipet.Domain.Intefaces
{
    public interface IProdutoHashtagRepository : IRepository<ProdutoHashtag>
    {
        Task<List<ProdutoHashtag>> ExcluirTagsDoProduto(Guid idProduto);
        Task<List<ProdutoHashtag>> ObterPorProdutoId(Guid produtoId);
    }
}