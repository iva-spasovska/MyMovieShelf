using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieShelf.Domain.DTO
{
    public class ReviewDTO
    {
        public Guid Id { get; set; }
        public Guid MovieId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
