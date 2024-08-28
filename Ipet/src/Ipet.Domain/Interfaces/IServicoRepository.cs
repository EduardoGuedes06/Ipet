using Ipet.Domain.Models;

namespace Ipet.Domain.Intefaces
{
    public interface IServicoRepository : IRepository<Servico>
    {
        Task<List<Servico>> GetServicosByTag(string[] tags, List<Servico> todosServicos);
    }
}