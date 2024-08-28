
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ipet.Data.Mappings
{
    public class FavoritoMapping : IEntityTypeConfiguration<Favorito>
    {
        public void Configure(EntityTypeBuilder<Favorito> builder)
        {
            builder.ToTable("Favoritos");

            builder.HasKey(p => p.Id);
        }
    }
}