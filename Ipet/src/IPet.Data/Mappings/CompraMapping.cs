
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ipet.Data.Mappings
{
    public class CompraMapping : IEntityTypeConfiguration<Compra>
    {
        public void Configure(EntityTypeBuilder<Compra> builder)
        {
            builder.ToTable("Compras");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.ListaProdutos)
                .IsRequired()
                .HasColumnType("varchar(5000)");
        }
    }
}