using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsManagement.Domain.Entities;

namespace ProductsManagement.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasColumnType("varchar")
                   .HasMaxLength(100);

            builder.Property(p => p.Price)
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Stock)
                   .IsRequired();

            builder.Property(p => p.Status)
                    .IsRequired();

            builder.Property(p => p.Description)
                   .IsRequired()
                   .HasColumnType("varchar")
                   .HasMaxLength(500);

            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
