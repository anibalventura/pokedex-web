using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database
{
	public class ApplicationContext : DbContext
	{
		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

		public DbSet<Pokemon> Pokemons { get; set; }
		public DbSet<Type> Types { get; set; }
		public DbSet<Region> Regions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Tables
            modelBuilder.Entity<Pokemon>().ToTable("Pokemons");
            modelBuilder.Entity<Type>().ToTable("Types");
            modelBuilder.Entity<Region>().ToTable("Regions");
            #endregion

            #region "Primary Keys"
            modelBuilder.Entity<Pokemon>().HasKey(h => h.Id);
            modelBuilder.Entity<Type>().HasKey(h => h.Id);
            modelBuilder.Entity<Region>().HasKey(h => h.Id);
            #endregion

            #region "Relationships"
            modelBuilder.Entity<Type>()
                .HasMany(s => s.PokemonsPrimary)
                .WithOne(g => g.PrimaryType)
                .HasForeignKey(s => s.PrimaryTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Type>()
                .HasMany(s => s.PokemonsSecondary)
                .WithOne(g => g.SecondaryType)
                .HasForeignKey(s => s.SecondaryTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Region>()
                .HasMany(s => s.Pokemons)
                .WithOne(g => g.Region)
                .HasForeignKey(s => s.RegionId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region "Property configurations"

            #region Pokemons
            modelBuilder.Entity<Pokemon>()
                .Property(p => p.Name)
                .IsRequired();

            modelBuilder.Entity<Pokemon>()
               .Property(p => p.ImageUrl)
               .IsRequired();

            modelBuilder.Entity<Pokemon>()
               .Property(p => p.PrimaryTypeId)
               .IsRequired();

            modelBuilder.Entity<Pokemon>()
               .Property(p => p.RegionId)
               .IsRequired();
            #endregion

            #region Types
            modelBuilder.Entity<Type>()
                .Property(p => p.Name)
                .IsRequired();
            #endregion

            #region Regions
            modelBuilder.Entity<Region>()
                .Property(p => p.Name)
                .IsRequired();
            #endregion

            #endregion
        }
    }
}
