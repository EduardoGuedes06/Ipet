

using Ipet.Domain.Models;

namespace Ipet.Domain.Intefaces
{
    public interface IProdutoService : IDisposable
    {
        Task<List<Produto>> GetProdutosByTags(string[] tags);
    }
}