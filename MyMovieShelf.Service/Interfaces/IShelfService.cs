using MyMovieShelf.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieShelf.Service.Interfaces
{
    public interface IShelfService
    {
        Shelf GetShelfForUser(string userId);
        void AddMovieToShelf(string userId, Guid movieId);
        void RemoveMovieFromShelf(string userId, Guid movieId);
        void ToggleWatched(string userId, Guid movieId);
    }
}
