using Ipet.Domain.Models;

namespace Ipet.Domain.Intefaces
{
    public interface IPerfilPetRepository : IRepository<PerfilPet>
    {
        Task<PerfilPet> ObterPerfilUsuario(Guid id);
    }
}