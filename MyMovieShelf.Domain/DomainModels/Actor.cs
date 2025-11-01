using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieShelf.Domain.DomainModels
{
    public class Actor : BaseEntity
    {
        public int TmdbId { get; set; }
        public string? FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? PlaceOfBirth { get; set; }          
        public string? Biography { get; set; }            
        public string? ProfileImageUrl { get; set; }
        public virtual ICollection<MovieActor>? MovieActors { get; set; }

    }
}
