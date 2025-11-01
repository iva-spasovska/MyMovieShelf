using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMovieShelf.Domain.DomainModels;
using MyMovieShelf.Repository;
using MyMovieShelf.Service.Implementations;
using MyMovieShelf.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyMovieShelf.Web.Controllers
{
    public class GenresController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly IMovieService _movieService;
        private readonly IShelfService _shelfService;
        public GenresController(IGenreService genreService, IMovieService movieService, IShelfService shelfService)
        {
            _genreService = genreService;
            _movieService = movieService;
            _shelfService = shelfService;
        }

        // GET: Genres/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = _genreService.GetById(id.Value);
            if (genre == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["Movies"] = _movieService.GetMoviesByGenre(id.Value, userId);

            return View(genre);
        }
    }
}
