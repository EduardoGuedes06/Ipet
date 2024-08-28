
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ipet.Data.Mappings
{
    public class PerfilPetMapping : IEntityTypeConfiguration<PerfilPet>
    {
        public void Configure(EntityTypeBuilder<PerfilPet> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Observacao)
                .IsRequired()
                .HasColumnType("varchar(1000)");


            builder.ToTable("PerfilPet");
        }
    }
}