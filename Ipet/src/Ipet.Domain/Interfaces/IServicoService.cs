

using Ipet.Domain.Models;

namespace Ipet.Domain.Intefaces
{
    public interface IServicoService : IDisposable
    {
        Task<List<Servico>> GetServicosByTags(string[] tags);
    }
}