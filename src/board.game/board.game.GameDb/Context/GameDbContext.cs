using Microsoft.EntityFrameworkCore;
using board.game.GameDb.Models;

namespace board.game.GameDb.Context
{

    public class GameDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Game> Games => Set<Game>();
        public DbSet<Publisher> Publishers => Set<Publisher>();
        public DbSet<Designer> Designers => Set<Designer>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Mechanic> Mechanics => Set<Mechanic>();
        public DbSet<GameDesigner> GameDesigners => Set<GameDesigner>();
        public DbSet<GameCategory> GameCategories => Set<GameCategory>();
        public DbSet<GameMechanic> GameMechanics => Set<GameMechanic>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Game -> Publisher (many-to-one)
            modelBuilder.Entity<Game>()
                .HasOne(g => g.Publisher)
                .WithMany(p => p.Games)
                .HasForeignKey(g => g.PublisherId)
                .OnDelete(DeleteBehavior.SetNull);

            // Game <-> Designer (many-to-many via GameDesigner)
            modelBuilder.Entity<GameDesigner>()
                .HasKey(gd => new { gd.GameId, gd.DesignerId });

            modelBuilder.Entity<GameDesigner>()
                .HasOne(gd => gd.Game)
                .WithMany(g => g.GameDesigners)
                .HasForeignKey(gd => gd.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GameDesigner>()
                .HasOne(gd => gd.Designer)
                .WithMany(d => d.GameDesigners)
                .HasForeignKey(gd => gd.DesignerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Game <-> Category (many-to-many via GameCategory)
            modelBuilder.Entity<GameCategory>()
                .HasKey(gc => new { gc.GameId, gc.CategoryId });

            modelBuilder.Entity<GameCategory>()
                .HasOne(gc => gc.Game)
                .WithMany(g => g.GameCategories)
                .HasForeignKey(gc => gc.GameId)
                .OnDelete(DeleteBehavior.Cascade);
           
            modelBuilder.Entity<GameCategory>()
                .HasOne(gc => gc.Category)
                .WithMany(c => c.GameCategories)
                .HasForeignKey(gc => gc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Game <-> Mechanic (many-to-many via GameMechanic)
            modelBuilder.Entity<GameMechanic>()
                .HasKey(gm => new { gm.GameId, gm.MechanicId });

            modelBuilder.Entity<GameMechanic>()
                .HasOne(gm => gm.Game)
                .WithMany(g => g.GameMechanics)
                .HasForeignKey(gm => gm.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GameMechanic>()
                .HasOne(gm => gm.Mechanic)
                .WithMany(m => m.GameMechanics)
                .HasForeignKey(gm => gm.MechanicId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
