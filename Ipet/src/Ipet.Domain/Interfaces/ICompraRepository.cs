using Ipet.Domain.Intefaces;
using Ipet.Domain.Models;

namespace Ipet.Data.Repository
{
    public interface ICompraRepository : IRepository<Compra>
    {
        Task<Compra> ObterCompraEmAbertoPorUsuario(Guid usuarioId);
        Task<List<Compra>> ObterComprasPorUsuario(Guid usuarioId);
        Task<List<Compra>> ObterComprasUltimos30Dias();
        //Task<int> ObterNumeroComprasUltimoMes(Guid produtoId);
    }
}