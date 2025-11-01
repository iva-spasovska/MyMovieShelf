using Microsoft.EntityFrameworkCore;
using MyMovieShelf.Domain.DomainModels;
using MyMovieShelf.Repository.Interfaces;
using MyMovieShelf.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieShelf.Service.Implementations
{
    public class ShelfService : IShelfService
    {
        private readonly IRepository<Shelf> _shelfRepository;
        private readonly IRepository<ShelfMovie> _shelfMovieRepository;
        private readonly IMovieService _movieService;

        public ShelfService(
            IRepository<Shelf> shelfRepository,
            IRepository<ShelfMovie> shelfMovieRepository,
            IMovieService movieService)
        {
            _shelfRepository = shelfRepository;
            _shelfMovieRepository = shelfMovieRepository;
            _movieService = movieService;
        }

        public Shelf GetShelfForUser(string userId)
        {
            var shelf = _shelfRepository.Get(selector: x => x,
                predicate: x => x.UserId == userId,
                include: x => x.Include(y => y.ShelfMovies)
                                    .ThenInclude(z => z.Movie)
                                        .ThenInclude(m => m.MovieGenres)
                                            .ThenInclude(n => n.Genre)
                                 .Include(y => y.ShelfMovies)
                                    .ThenInclude(z => z.Movie)
                                        .ThenInclude(m => m.MovieActors)
                                            .ThenInclude(n => n.Actor));

            if (shelf == null)
            {
                shelf = new Shelf
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    ShelfMovies = new List<ShelfMovie>()
                };
                _shelfRepository.Insert(shelf);
            }

            return shelf;
        }

        public void AddMovieToShelf(string userId, Guid movieId)
        {
            var shelf = GetShelfForUser(userId);

            shelf.ShelfMovies ??= new List<ShelfMovie>();

            if (!shelf.ShelfMovies.Any(x => x.MovieId == movieId))
            {
                var shelfMovie = new ShelfMovie
                {
                    Id = Guid.NewGuid(),
                    ShelfId = shelf.Id,
                    MovieId = movieId,
                    IsWatched = false,
                    AddedOn = DateTime.Now
                };

                _shelfMovieRepository.Insert(shelfMovie);
            }
        }

        public void RemoveMovieFromShelf(string userId, Guid movieId)
        {
            var shelf = GetShelfForUser(userId);
            var shelfMovie = shelf.ShelfMovies.FirstOrDefault(x => x.MovieId == movieId);

            if (shelfMovie != null)
            {
                _shelfMovieRepository.Delete(shelfMovie);
            }
        }

        public void ToggleWatched(string userId, Guid movieId)
        {
            var shelf = GetShelfForUser(userId);
            var shelfMovie = shelf.ShelfMovies.FirstOrDefault(sm => sm.MovieId == movieId);

            if (shelfMovie != null)
            {
                shelfMovie.IsWatched = !shelfMovie.IsWatched;
                _shelfMovieRepository.Update(shelfMovie);
            }
        }
    }
}