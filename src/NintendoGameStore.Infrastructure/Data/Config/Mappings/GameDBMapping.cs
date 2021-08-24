using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NintendoGameStore.Core.Models;

namespace NintendoGameStore.Infrastructure.Data.Config.Mappings
{
    internal class GameDBMapping : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("Games");

            builder.HasKey(g => g.Id);
            builder.HasIndex(g => g.Name);

            builder.Property(g => g.Id).HasColumnName("GameID");

            builder.Property(g => g.Name).HasColumnName("Name")
                                        .HasColumnType("varchar")
                                        .HasMaxLength(100);

            builder.Property(g => g.Description).HasColumnName("Description")
                                                .HasColumnType("varchar")
                                                .HasMaxLength(500);

            builder.Property(g => g.Price).HasColumnName("Price");
            builder.Property(g => g.ReleaseDate).HasColumnName("ReleaseDate");
            builder.Property(g => g.NumberOfPlayers).HasColumnName("NumberOfPlayers");

            builder.HasMany(g => g.Categories)
                  .WithMany(c => c.Games);
        }
    }
}
