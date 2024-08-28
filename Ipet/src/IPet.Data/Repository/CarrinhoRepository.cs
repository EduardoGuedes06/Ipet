using Ipet.Data.Context;
using Ipet.Domain.Intefaces;
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Ipet.Data.Repository
{
    public class CarrinhoRepository : Repository<Carrinho>, ICarrinhoRepository
    {
        public CarrinhoRepository(MeuDbContext context) : base(context) { }

        public async Task<Carrinho> ObterTodoCarrinhoPorId(Guid id)
        {
            return await Db.Carrinhos
                .Include(c => c.CarrinhoProdutos)
                    .ThenInclude(cp => cp.Produto)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Carrinho> ObterCarrinhoPorUsuario(Guid usuarioId)
        {
            return await Db.Carrinhos
                .Include(c => c.CarrinhoProdutos)
                    .ThenInclude(cp => cp.Produto)
                .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);
        }
        public async Task<bool> ObterUsuarioPorId(Guid usuarioId)
        {
            return await Db.Carrinhos.AnyAsync(c => c.UsuarioId == usuarioId);
        }

    }
}