using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ipet.ViewModels;

namespace Ipet.MVC.Data
{
    public class IpetMVCContext : DbContext
    {
        public IpetMVCContext (DbContextOptions<IpetMVCContext> options)
            : base(options)
        {
        }

        public DbSet<Ipet.ViewModels.CarrinhoViewModel> CarrinhoViewModel { get; set; } = default!;
    }
}
