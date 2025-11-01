using MyMovieShelf.Domain.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieShelf.Domain.DomainModels
{
    public class Review : BaseEntity
    {
        public Guid MovieId { get; set; }
        public Movie? Movie { get; set; }

        public string? UserId { get; set; }
        public MovieApplicationUser? User { get; set; }

        public int Rating { get; set; } // 1 to 10
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
