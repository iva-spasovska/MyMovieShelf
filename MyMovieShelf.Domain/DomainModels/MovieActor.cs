using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieShelf.Domain.DomainModels
{
    public class MovieActor : BaseEntity
    {
        public Guid MovieId { get; set; }
        public Movie? Movie { get; set; }

        public Guid ActorId { get; set; }
        public Actor? Actor { get; set; }
    }
}
