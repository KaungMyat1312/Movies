using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace MVCMovie.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MovieContext(
            serviceProvider.GetRequiredService<DbContextOptions<MovieContext>>()))
        {
            if (context.Movies.Any())
            {
                return;  
            }

            context.Movies.AddRange(
                new Movie
                {
                    Title = "When Harry Met Sally",
                    Description = "Harry and Sally have known each other for years, and are very good friends, but they fear sex would ruin the friendship.",
                    ReleaseDate = DateTime.Parse("1989-2-12"),
                    Duration = 95,
                    Rating = "R",
                    PosterUrl = "https://images.unsplash.com/photo-1536440136628-849c177e76a1?w=500", 
                    TrailerUrl = "https://www.youtube.com/watch?v=V8DgDmUHVto",
                    IsActive = true
                },
                new Movie
                {
                    Title = "Ghostbusters",
                    Description = "Three eccentric parapsychologists start a ghost-catching business in New York City.",
                    ReleaseDate = DateTime.Parse("1984-6-8"),
                    Duration = 105,
                    Rating = "PG",
                    PosterUrl = "https://images.unsplash.com/photo-1489599849927-2ee91cede3ba?w=500",
                    TrailerUrl = "https://www.youtube.com/watch?v=6hDkhw5WKas",
                    IsActive = true
                },
                new Movie
                {
                    Title = "Ghostbusters 2",
                    Description = "The discovery of a massive river of ectoplasm and a revival of spectral activity allows the staff of Ghostbusters to revive the business.",
                    ReleaseDate = DateTime.Parse("1989-6-16"),
                    Duration = 108,
                    Rating = "PG",
                    PosterUrl = "https://images.unsplash.com/photo-1489599849927-2ee91cede3ba?w=500",
                    TrailerUrl = "https://www.youtube.com/watch?v=UnzH75IFwwU",
                    IsActive = true
                }
            );

            context.SaveChanges();
        }
    }
}