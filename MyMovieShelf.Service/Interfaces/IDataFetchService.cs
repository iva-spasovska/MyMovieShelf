using MyMovieShelf.Domain.DomainModels;
using MyMovieShelf.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieShelf.Service.Interfaces
{
    public interface IDataFetchService
    {
        Task<List<MovieSearchResultDTO>> SearchMovies(string query);
        Task<Movie> FetchMovieDetails(int tmdbMovieId);
        Task<MovieDetailsDTO> GetMovieDetailsFromApi(int tmdbId);
        Task<List<MovieSearchResultDTO>> GetPopularMovies();
        Task<Actor> FetchActorDetails(int tmdbId);
    }
}
