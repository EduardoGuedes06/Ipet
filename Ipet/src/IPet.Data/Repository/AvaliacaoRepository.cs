using Ipet.Data.Context;
using Ipet.Domain.Intefaces;
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipet.Data.Repository
{
    public class AvaliacaoRepository : Repository<Avaliacao>, IAvaliacaoRepository
    {
        private readonly MeuDbContext _dbContext;
        public AvaliacaoRepository(MeuDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<double> ObterMediaAvaliacaoProduto(Guid produtoId)
        {
            return _dbContext.Avaliacoes
                           .Where(i => i.ProdutoId == produtoId)
                           .Select(i => i.Estrelas).Average();

        }
        public async Task<Avaliacao> ObterAvaliacaoProdutoUsuario(Guid produtoId, Guid userId)
        {
            return await _dbContext.Avaliacoes
                                   .FirstOrDefaultAsync(i => i.ProdutoId == produtoId && i.UserId == userId);
        }
        public async Task<List<Avaliacao>> ObterAvaliacoesPorProduto(Guid produtoId)
        {
            return await _dbContext.Avaliacoes
                   .Where(i => i.ProdutoId == produtoId)
                   .ToListAsync();
        }
        public async Task ExcluirAvaliacaoPorProdutoId(Guid produtoId)
        {
            var avaliacao = await _dbContext.Avaliacoes.FirstOrDefaultAsync(a => a.ProdutoId == produtoId);

            if (avaliacao != null)
            {
                _dbContext.Avaliacoes.Remove(avaliacao);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
