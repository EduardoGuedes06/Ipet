using Ipet.Data.Context;
using Ipet.Domain.Intefaces;
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Ipet.Data.Repository
{
    public class ProdutoHashtagRepository : Repository<ProdutoHashtag>, IProdutoHashtagRepository
    {
        public ProdutoHashtagRepository(MeuDbContext context) : base(context) { }

        public async Task<List<ProdutoHashtag>> ObterPorProdutoId(Guid produtoId)
        {
            return await DbSet.Where(h => h.IdProduto == produtoId).ToListAsync();
        }
        public async Task<List<ProdutoHashtag>> ExcluirTagsDoProduto(Guid idProduto)
        {
            var tagsDoProduto = await Buscar(tag => tag.IdProduto == idProduto);

            if (tagsDoProduto.Any())
            {
                foreach (var tag in tagsDoProduto)
                {
                    await Remover(tag.Id); // Chama o método genérico de remoção da classe base
                }
            }

            return null;
        }

    }
}