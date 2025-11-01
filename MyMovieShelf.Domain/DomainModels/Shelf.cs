using MyMovieShelf.Domain.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieShelf.Domain.DomainModels
{
    public class Shelf : BaseEntity
    {
        public string? UserId { get; set; }
        public MovieApplicationUser? User { get; set; }

        public virtual ICollection<ShelfMovie>? ShelfMovies { get; set; }
    }

}
