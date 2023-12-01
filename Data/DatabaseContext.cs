using Entity;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class DatabaseContext : DbContext {
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<MovieEntity> Movies { get; set; }
    public DbSet<WishListEntity> WishLists { get; set; }

    public DbSet<ReviewEntity> Reviews { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseExceptionProcessor();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<WishListEntity>().HasKey(entity => new {entity.MovieId, entity.Username});
        modelBuilder.Entity<ReviewEntity>().HasKey(entity => new {entity.MovieId, entity.Author});



        // Building WishListEntity
        modelBuilder.Entity<WishListEntity>()
            .HasOne(w => w.User)
            .WithMany(u => u.WishLists)
            .HasForeignKey(w => w.Username)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WishListEntity>()
            .HasOne(w => w.Movie)
            .WithMany(m => m.WishLists)
            .HasForeignKey(w => w.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        

        // Building ReviewEntity
        modelBuilder.Entity<ReviewEntity>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.Author)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ReviewEntity>()
            .HasOne(r => r.Movie)
            .WithMany(m => m.Reviews)
            .HasForeignKey(r => r.MovieId)
            .OnDelete(DeleteBehavior.Cascade);



        base.OnModelCreating(modelBuilder);
    }
}