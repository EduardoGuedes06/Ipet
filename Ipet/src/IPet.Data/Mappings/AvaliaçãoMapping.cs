using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ipet.Data.Mappings
{
    public class AvaliaçãoMapping : IEntityTypeConfiguration<Avaliacao>
    {
        public void Configure(EntityTypeBuilder<Avaliacao> builder)
        {
            builder.ToTable("Avaliação");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProdutoId);
            builder.Property(x=>x.Estrelas);
        }
    }
}
