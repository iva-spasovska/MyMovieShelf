using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMovieShelf.Domain.DomainModels;
using MyMovieShelf.Domain.DTO;
using MyMovieShelf.Repository;
using MyMovieShelf.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyMovieShelf.Web.Controllers
{
    [Authorize]
    public class ShelvesController : Controller
    {
        private readonly IShelfService _shelfService;
        private readonly IDataFetchService _dataFetchService;
        private readonly IMovieService _movieService;
        private readonly IReviewService _reviewService;

        public ShelvesController(IShelfService shelfService, IDataFetchService dataFetchService, IMovieService movieService, IReviewService reviewService)
        {
            _shelfService = shelfService;
            _dataFetchService = dataFetchService;
            _movieService = movieService;
            _reviewService = reviewService;
        }

        // GET: Shelves
        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var shelf = _shelfService.GetShelfForUser(userId);
            return View(shelf);
        }

        // GET: Shelves/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = _movieService.GetById(id.Value);
            if (movie == null)
            {
                return NotFound();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var shelf = _shelfService.GetShelfForUser(userId);
            var shelfMovie = shelf.ShelfMovies.FirstOrDefault(sm => sm.MovieId == movie.Id);

            ViewData["IsWatched"] = shelfMovie?.IsWatched ?? false;

            var reviews = _reviewService.GetReviewsForMovie(id.Value);
            ViewData["Reviews"] = reviews;

            return View(movie);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return View(new List<MovieSearchResultDTO>());

            var results = await _dataFetchService.SearchMovies(query);
            return View(results);
        }

        [HttpPost]
        public async Task<IActionResult> AddFromTmdb(int tmdbId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var movie = await _dataFetchService.FetchMovieDetails(tmdbId);
            
            _shelfService.AddMovieToShelf(userId, movie.Id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(Guid movieId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _shelfService.RemoveMovieFromShelf(userId, movieId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ToggleWatched(Guid movieId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _shelfService.ToggleWatched(userId, movieId);
            return RedirectToAction("Details", new { id = movieId });
        }

        [HttpGet]
        public async Task<IActionResult> TmdbDetails(int tmdbId)
        {
            var movieDetails = await _dataFetchService.GetMovieDetailsFromApi(tmdbId);
            return View(movieDetails);
        }
    }
}
