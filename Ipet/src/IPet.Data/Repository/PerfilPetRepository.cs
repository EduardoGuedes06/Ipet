using Ipet.Data.Context;
using Ipet.Domain.Intefaces;
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Ipet.Data.Repository
{
    public class PerfilPetRepository : Repository<PerfilPet>, IPerfilPetRepository
    {
        public PerfilPetRepository(MeuDbContext context) : base(context) { }


        public async Task<PerfilPet> ObterPerfilUsuario(Guid idUser)
        {
            return await Db.PerfilPet.AsNoTracking()
                .FirstOrDefaultAsync(p => p.IdUsuario == idUser);
        }
    }

}