using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieShelf.Domain.DomainModels
{
    public class ShelfMovie : BaseEntity
    {
        public Guid ShelfId { get; set; }
        public Shelf? Shelf { get; set; }

        public Guid MovieId { get; set; }
        public Movie? Movie { get; set; }

        public bool IsWatched { get; set; } 
        public DateTime AddedOn { get; set; } = DateTime.Now;
    }

}
