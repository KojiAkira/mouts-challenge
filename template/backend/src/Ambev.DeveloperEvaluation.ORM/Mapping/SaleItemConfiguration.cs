using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItem");

            builder.HasKey(si => si.Id);
            builder.Property(si => si.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(si => si.SaleId).HasColumnType("uuid").IsRequired();

            builder.HasOne<Sale>()
                   .WithMany(s => s.Items) 
                   .HasForeignKey(si => si.SaleId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(si => si.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(si => si.UnitPrice).HasPrecision(18, 2).IsRequired();
            builder.Property(si => si.Discount).HasPrecision(18, 2).IsRequired();
            builder.Property(si => si.TotalAmount).HasPrecision(18, 2).IsRequired();
        }
    }
}
