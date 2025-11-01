using Microsoft.AspNetCore.Identity;
using MyMovieShelf.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieShelf.Domain.IdentityModels
{
    public class MovieApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
        public Shelf? Shelves { get; set; }
    }
}
