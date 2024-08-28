using Ipet.Domain.Models;

namespace Ipet.Domain.Intefaces
{
    public interface ICarrinhoRepository : IRepository<Carrinho>
    {
        Task<Carrinho> ObterCarrinhoPorUsuario(Guid usuarioId);
        Task<Carrinho> ObterTodoCarrinhoPorId(Guid id);
        Task<bool> ObterUsuarioPorId(Guid usuarioId);
    }
}