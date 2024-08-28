using Ipet.Data.Context;
using Ipet.Domain.Intefaces;
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Ipet.Data.Repository
{
    public class CarrinhoProdutoRepository : Repository<CarrinhoProduto>, ICarrinhoProdutoRepository
    {
        public CarrinhoProdutoRepository(MeuDbContext context) : base(context) { }

        public async Task<Guid> ObterCarrinhoIdPorUsuarioId(Guid usuarioId)
        {
            var carrinho = await Db.Carrinhos
                                    .Where(c => c.UsuarioId == usuarioId)
                                    .Select(c => c.Id)
                                    .FirstOrDefaultAsync();

            return carrinho;
        }
        public async Task<bool> RemoverprodutoPorCarrinho(Guid carrinhoId, Guid produtoId)
        {
            var carrinhoProduto = await Db.CarrinhoProdutos
                                            .FirstOrDefaultAsync(cp => cp.CarrinhoId == carrinhoId && cp.ProdutoId == produtoId);

            if (carrinhoProduto == null)
                return false;

            Db.CarrinhoProdutos.Remove(carrinhoProduto);
            await Db.SaveChangesAsync();

            return true;
        }
        //Delete Prod
        public async Task<bool> RemoverProdutosDoCarrinho(Guid produtoId)
        {
            var carrinhoProdutos = await Db.CarrinhoProdutos
                                            .Where(cp => cp.ProdutoId == produtoId)
                                            .ToListAsync();

            if (carrinhoProdutos == null || !carrinhoProdutos.Any())
                return false;

            Db.CarrinhoProdutos.RemoveRange(carrinhoProdutos);
            await Db.SaveChangesAsync();

            return true;
        }
        public async Task<bool> ProdutoExisteEmCarrinho(Guid produtoId)
        {
            var produtoExiste = await Db.CarrinhoProdutos
                                         .AnyAsync(cp => cp.ProdutoId == produtoId);

            return produtoExiste;
        }
    }
}