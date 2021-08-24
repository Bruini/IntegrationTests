using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NintendoGameStore.Core.Models;

namespace NintendoGameStore.Infrastructure.Data.Config.Mappings
{
    internal class CategoryDBMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.Name);

            builder.Property(c => c.Id).HasColumnName("CategoryID");

            builder.Property(c => c.Name).HasColumnName("Name")
                                        .HasColumnType("varchar")
                                        .HasMaxLength(100);

            builder.HasMany(c => c.Games)
                .WithMany(g => g.Categories);
        }
    }
}
