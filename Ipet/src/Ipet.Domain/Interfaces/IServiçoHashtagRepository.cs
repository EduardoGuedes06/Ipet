using Ipet.Domain.Models;

namespace Ipet.Domain.Intefaces
{
    public interface IServiçoHashtagRepository : IRepository<ServiçoHashtag>
    {
        Task ExcluirTagsDoServico(Guid idServico);
        Task<List<ServiçoHashtag>> ObterPorServicoId(Guid servicoId);
    }
}