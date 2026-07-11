using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MVCMovie.Models;


    public class MovieContext:IdentityDbContext<ApplicationUser>
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        { }
        public DbSet<Movie> Movies { get; set; } 
        public DbSet<Category> Categories { get; set; }
        public DbSet<MovieCategory> MovieCategories { get; set; }

    public DbSet<UserFavourite> UserFavourites { get; set; }
    public DbSet<UserSave> UserSaves { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MovieCategory>()
                .HasKey(mc => new { mc.MovieId, mc.CategoryId });

            modelBuilder.Entity<MovieCategory>()
                .HasOne(mc => mc.Movie)
                .WithMany(m => m.MovieCategories)
                .HasForeignKey(mc => mc.MovieId);



        // UserFavourite Many-to-Many Mapping
        modelBuilder.Entity<UserFavourite>()
            .HasKey(uf => new { uf.UserId, uf.MovieId });

        modelBuilder.Entity<UserFavourite>()
            .HasOne(uf => uf.User)
            .WithMany(u => u.UserFavourites)
            .HasForeignKey(uf => uf.UserId);

        modelBuilder.Entity<UserFavourite>()
            .HasOne(uf => uf.Movie)
            .WithMany(m => m.UserFavourites)
            .HasForeignKey(uf => uf.MovieId);

        // UserSave Many-to-Many Mapping
        modelBuilder.Entity<UserSave>()
            .HasKey(us => new { us.UserId, us.MovieId });

        modelBuilder.Entity<UserSave>()
            .HasOne(us => us.User)
            .WithMany(u => u.UserSaves)
            .HasForeignKey(us => us.UserId);

        modelBuilder.Entity<UserSave>()
            .HasOne(us => us.Movie)
            .WithMany(m => m.UserSaves)
            .HasForeignKey(us => us.MovieId);
    }
    }
