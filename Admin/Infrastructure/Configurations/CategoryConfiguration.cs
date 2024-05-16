using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Categories;

namespace Infrastructure.Configurations;

internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(o => o.Id).HasConversion(
            category => category.Value,
            value => new CategoryId(value));

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.Slug)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.HasIndex(x => x.Slug).IsUnique();

        builder.Property(x => x.Icon)
            .HasMaxLength(100)
            .IsRequired();
    }
}