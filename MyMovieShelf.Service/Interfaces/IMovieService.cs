using MyMovieShelf.Domain.DomainModels;
using MyMovieShelf.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieShelf.Service.Interfaces
{
    public interface IMovieService
    {
        List<Movie> GetAll();
        Movie? GetById(Guid Id);

        Movie Create(Movie movie);

        List<Movie> GetMoviesByActor(Guid actorId, string userId);
        List<Movie> GetMoviesByGenre(Guid genreId, string userId);
    }
}
