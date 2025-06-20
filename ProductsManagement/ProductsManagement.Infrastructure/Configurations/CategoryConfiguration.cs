using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsManagement.Domain.Entities;

namespace ProductsManagement.Infrastructure.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);
        }
    }
}
