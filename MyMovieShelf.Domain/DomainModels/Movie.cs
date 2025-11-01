using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieShelf.Domain.DomainModels
{
    public class Movie : BaseEntity
    {
        public string? Title { get; set; }
        public string? Overview { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? PosterUrl { get; set; }
        public bool IsWatched { get; set; }

        public int TmdbId { get; set; }

        public virtual ICollection<MovieActor>? MovieActors { get; set; }
        public virtual ICollection<MovieGenre>? MovieGenres { get; set; }
        public virtual ICollection<ShelfMovie>? ShelfMovies { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
        
    }
}
