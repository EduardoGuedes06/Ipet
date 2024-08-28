using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ipet.ViewModels;

namespace Ipet.MVC.Data
{
    public class IpetMVCContext2222 : DbContext
    {
        public IpetMVCContext2222 (DbContextOptions<IpetMVCContext2222> options)
            : base(options)
        {
        }

        public DbSet<Ipet.ViewModels.AvaliacaoViewModel> AvaliacaoViewModel { get; set; } = default!;
    }
}
