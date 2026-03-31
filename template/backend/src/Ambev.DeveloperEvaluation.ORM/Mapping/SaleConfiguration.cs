using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sale");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            // Limpo e direto, sem repetição
            builder.Property(s => s.CustomerId).HasColumnType("uuid").IsRequired();
            builder.Property(s => s.CustomerName).HasMaxLength(150).IsRequired(); // Desnormalização

            builder.Property(s => s.BranchId).HasColumnType("uuid").IsRequired();
            builder.Property(s => s.BranchName).HasMaxLength(150).IsRequired(); // Desnormalização

            builder.Property(s => s.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            // Define a precisão do campo financeiro
            builder.Property(s => s.TotalAmount).HasPrecision(18, 2);

            builder.HasIndex(s => s.CustomerId);
            builder.HasIndex(s => s.BranchId);
        }
    }
}