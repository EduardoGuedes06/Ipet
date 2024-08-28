using Ipet.Domain.Intefaces;
using Ipet.Domain.Models;

namespace Ipet.Data.Repository
{
    public interface IFavoritoRepository : IRepository<Favorito>
    {
        Task<List<Favorito>> ObterFavoritosPorUsuario(Guid usuarioId);
        Task RemoverFavoritoPorUsuario(Guid idUsuario, Guid idProduto);
    }
}