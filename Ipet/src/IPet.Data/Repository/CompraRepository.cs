using Ipet.Data.Context;
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Ipet.Data.Repository
{
    public class CompraRepository : Repository<Compra>, ICompraRepository
    {
        private readonly MeuDbContext dbContext;

        public CompraRepository(MeuDbContext context) : base(context) { dbContext = context; }

        public async Task<Compra> ObterCompraEmAbertoPorUsuario(Guid usuarioId)
        {
            return await dbContext.Compras
                   .Where(i => i.UsuarioId == usuarioId && i.Status == "Aguardando Pagamento" || i.Status == "Pagamento Realizado")
                   .FirstOrDefaultAsync();
        }

        public async Task<List<Compra>> ObterComprasPorUsuario(Guid usuarioId)
        {
            return await dbContext.Compras
                .Where(i => i.UsuarioId == usuarioId)
                .OrderBy(c => c.DataCompra == DateTime.MinValue ? 0 : 1) // Trata as datas com valor mínimo primeiro
                .ThenByDescending(c => c.DataCompra) // Ordena as demais datas normalmente
                .ToListAsync();
        }

        public async Task<List<Compra>> ObterComprasUltimos30Dias()
        {
            // Obtém a data de 30 dias atrás a partir de agora
            DateTime dataInicio = DateTime.Now.AddDays(-30);

            // Obtém as compras dos últimos 30 dias
            var comprasUltimos30Dias = await dbContext.Compras
                .Where(c => c.DataCompra >= dataInicio && c.Status == "Aprovado")
                .ToListAsync();

            return comprasUltimos30Dias;
        }






    }
}
