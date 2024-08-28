using Ipet.Data.Context;
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Ipet.Data.Repository
{
    public class FavoritoRepository : Repository<Favorito>, IFavoritoRepository
    {
        private readonly MeuDbContext dbContext;

        public FavoritoRepository(MeuDbContext context) : base(context) { dbContext = context; }

        public async Task<List<Favorito>> ObterFavoritosPorUsuario(Guid usuarioId)
        {
            return await dbContext.Favoritos
                   .Where(i => i.UsuarioId == usuarioId)
                   .ToListAsync();
        }

        public async Task RemoverFavoritoPorUsuario(Guid idUsuario, Guid idProduto)
        {
            var entity = await dbContext.Favoritos
                         .FirstOrDefaultAsync(i => i.UsuarioId == idUsuario && i.ProdutoId == idProduto) ;

            DbSet.Remove(entity);
            await SaveChanges();
        }

    }
}
