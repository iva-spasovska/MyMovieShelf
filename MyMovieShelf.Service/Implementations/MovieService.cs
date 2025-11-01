using Microsoft.EntityFrameworkCore;
using MyMovieShelf.Domain.DomainModels;
using MyMovieShelf.Domain.DTO;
using MyMovieShelf.Repository.Interfaces;
using MyMovieShelf.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieShelf.Service.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _repository;
        private readonly IGenreService _genreService;
        private readonly IActorService _actorService;

        public MovieService(IRepository<Movie> repository, IGenreService genreService, IActorService actorService)
        {
            _repository = repository;
            _genreService = genreService;
            _actorService = actorService;
        }

        public Movie Create(Movie movie)
        {
            return _repository.Insert(movie);
        }


        public List<Movie> GetAll()
        {
            return _repository.GetAll(selector: x => x,
                include: x => x.Include(y => y.MovieGenres).ThenInclude(z => z.Genre)
                    .Include(y => y.MovieActors).ThenInclude(z => z.Actor)).ToList();
        }

        public Movie? GetById(Guid Id)
        {
            return _repository.Get(selector: x => x,
                predicate: x => x.Id == Id,
                include: x => x.Include(y => y.MovieGenres).ThenInclude(z => z.Genre)
                    .Include(y => y.MovieActors).ThenInclude(z => z.Actor));
        }

        public List<Movie> GetMoviesByActor(Guid actorId, string userId)
        {
            return _repository.GetAll(
                selector: x => x,
                predicate: x => x.MovieActors.Any(ma => ma.ActorId == actorId) &&
                                 x.ShelfMovies.Any(sm => sm.Shelf.UserId == userId),
                include: x => x
                    .Include(m => m.MovieActors)
                        .ThenInclude(ma => ma.Actor)
                    .Include(m => m.ShelfMovies)
                        .ThenInclude(sm => sm.Shelf)
            ).ToList();
        }


        public List<Movie> GetMoviesByGenre(Guid genreId, string userId)
        {
            return _repository.GetAll(selector: x => x,
                predicate: x => x.MovieGenres.Any(y => y.GenreId == genreId) &&
                                x.ShelfMovies.Any(z => z.Shelf.UserId == userId),
                include: x => x.Include(y => y.MovieGenres).ThenInclude(z => z.Genre)
                                .Include(y => y.ShelfMovies).ThenInclude(z => z.Shelf)
                ).ToList();
        }

    }
}
