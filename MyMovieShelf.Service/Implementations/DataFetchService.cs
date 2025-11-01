using Humanizer;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using MyMovieShelf.Domain.DomainModels;
using MyMovieShelf.Domain.DTO;
using MyMovieShelf.Service.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MyMovieShelf.Service.Implementations
{
    public class DataFetchService : IDataFetchService
    {
        private readonly HttpClient _httpClient;
        private readonly IMovieService _movieService;
        private readonly IActorService _actorService;
        private readonly IGenreService _genreService;
        private readonly string _apiKey;

        public DataFetchService(IHttpClientFactory httpClientFactory, IMovieService movieService, IActorService actorService, IGenreService genreService, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _movieService = movieService;
            _actorService = actorService;
            _genreService = genreService;
            _httpClient.BaseAddress = new Uri("https://api.themoviedb.org/3/");
            _apiKey = configuration["ApiSettings:ApiKey"];
        }

        public async Task<MovieDetailsDTO> GetMovieDetailsFromApi(int tmdbId)
        {
            var url = $"movie/{tmdbId}?api_key={_apiKey}&append_to_response=credits";
            var json = await _httpClient.GetStringAsync(url);
            var movieDetails = JsonConvert.DeserializeObject<MovieDetailsDTO>(json);

            return movieDetails!;
        }

        public async Task<Movie> FetchMovieDetails(int tmdbId)
        {
            var existingMovie = _movieService.GetAll().FirstOrDefault(m => m.TmdbId == tmdbId);

            if (existingMovie != null)
            {
                return existingMovie;
            }

            var url = $"movie/{tmdbId}?api_key={_apiKey}&append_to_response=credits";
            var json = await _httpClient.GetStringAsync(url);
            var dto = JsonConvert.DeserializeObject<MovieDetailsDTO>(json);

            var movie = new Movie
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Overview = dto.Overview,
                ReleaseDate = dto.ReleaseDate,
                PosterUrl = string.IsNullOrWhiteSpace(dto.PosterPath)
                    ? null
                    : $"https://image.tmdb.org/t/p/w500{dto.PosterPath}",
                IsWatched = false,
                TmdbId = tmdbId,
                MovieGenres = new List<MovieGenre>(),
                MovieActors = new List<MovieActor>()
            };

            if (dto.Genres != null)
            {
                foreach (var genreDto in dto.Genres)
                {
                    var existingGenre = _genreService.GetAll()
                        .FirstOrDefault(g => g.Name.Equals(genreDto.Name, StringComparison.OrdinalIgnoreCase));

                    if (existingGenre == null)
                    {
                        existingGenre = new Genre
                        {
                            Id = Guid.NewGuid(),
                            Name = genreDto.Name
                        };
                        _genreService.Create(existingGenre);
                    }

                    movie.MovieGenres.Add(new MovieGenre
                    {
                        MovieId = movie.Id,
                        GenreId = existingGenre.Id
                    });
                }
            }
            
            if (dto.Credits?.Cast != null)
            {
                foreach (var actorDto in dto.Credits.Cast.Take(10)) // limit to first 10 actors
                {
                    if (!string.IsNullOrWhiteSpace(actorDto.FullName))
                    {
                        var existingActor = _actorService.GetAll()
                            .FirstOrDefault(a =>
                                a.FullName != null &&
                                a.FullName.Equals(actorDto.FullName, StringComparison.OrdinalIgnoreCase)
                            );

                        if (existingActor == null)
                        {
                            existingActor = new Actor
                            {
                                Id = Guid.NewGuid(),
                                FullName = actorDto.FullName,
                                TmdbId = actorDto.TmdbId
                            };
                            _actorService.Create(existingActor);
                        }

                        movie.MovieActors.Add(new MovieActor
                        {
                            MovieId = movie.Id,
                            ActorId = existingActor.Id
                        });
                    }
                }
            }

            _movieService.Create(movie);
            return movie;
        }

        public async Task<List<MovieSearchResultDTO>> SearchMovies(string query)
        {
            var url = $"search/movie?api_key={_apiKey}&query={Uri.EscapeDataString(query)}";
            var json = await _httpClient.GetStringAsync(url);

            var response = JsonConvert.DeserializeObject<MovieSearchResponseDTO>(json);

            return response?.Results?.Select(m => new MovieSearchResultDTO
            {
                TmdbId = m.TmdbId,
                Title = m.Title,
                Overview = m.Overview,
                PosterPath = string.IsNullOrWhiteSpace(m.PosterPath)
                    ? null
                    : $"https://image.tmdb.org/t/p/w500{m.PosterPath}",
                ReleaseDateRaw = m.ReleaseDate?.ToString("yyyy-MM-dd")
            }).ToList() ?? new List<MovieSearchResultDTO>();
        }

        public async Task<List<MovieSearchResultDTO>> GetPopularMovies()
        {
            var url = $"movie/popular?api_key={_apiKey}&language=en-US&page=1";
            var json = await _httpClient.GetStringAsync(url);

            var response = JsonConvert.DeserializeObject<MovieSearchResponseDTO>(json);

            return response?.Results?.Take(18).Select(m => new MovieSearchResultDTO
            {
                TmdbId = m.TmdbId,
                Title = m.Title,
                Overview = m.Overview,
                PosterPath = string.IsNullOrWhiteSpace(m.PosterPath)
                    ? null
                    : $"https://image.tmdb.org/t/p/w500{m.PosterPath}",
                ReleaseDateRaw = m.ReleaseDate?.ToString("yyyy-MM-dd")
            }).ToList() ?? new List<MovieSearchResultDTO>();
        }

        public async Task<Actor> FetchActorDetails(int tmdbId)
        {
            var actor = _actorService.GetAll().FirstOrDefault(a => a.TmdbId == tmdbId);
            if (actor == null)
                return null; // or throw, depending on your workflow

            var url = $"https://api.themoviedb.org/3/person/{tmdbId}?api_key={_apiKey}&language=en-US";
            var json = await _httpClient.GetStringAsync(url);
            var dto = JsonConvert.DeserializeObject<ActorDTO>(json);

            actor.FullName = dto.FullName;
            actor.Biography = dto.Biography;
            actor.ProfileImageUrl = string.IsNullOrWhiteSpace(dto.ProfilePath)
                ? null
                : $"https://image.tmdb.org/t/p/w500{dto.ProfilePath}";
            if (DateTime.TryParse(dto.Birthday, out var birthDate))
            {
                actor.BirthDate = birthDate;
            }
            else
            {
                actor.BirthDate = null; 
            }


            _actorService.Update(actor);

            return actor;
        }

    }
}
