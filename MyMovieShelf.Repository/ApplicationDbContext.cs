using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyMovieShelf.Domain.DomainModels;
using MyMovieShelf.Domain.IdentityModels;

namespace MyMovieShelf.Repository
{
    public class ApplicationDbContext : IdentityDbContext<MovieApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<MovieGenre> MovieGenres { get; set; }
        public virtual DbSet<MovieActor> MovieActors { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Shelf> Shelves { get; set; }
        public virtual DbSet<ShelfMovie> ShelfMovies { get; set; }

    }
}
