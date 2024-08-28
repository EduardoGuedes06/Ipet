using Ipet.Domain.Models;

namespace Ipet.Domain.Intefaces
{
    public interface IAvaliacaoRepository : IRepository<Avaliacao>
    {
        Task<double> ObterMediaAvaliacaoProduto(Guid produtoId);
        Task<List<Avaliacao>> ObterAvaliacoesPorProduto(Guid produtoId);
        Task<Avaliacao> ObterAvaliacaoProdutoUsuario(Guid produtoId, Guid userId);
        Task ExcluirAvaliacaoPorProdutoId(Guid produtoId);
    }
}