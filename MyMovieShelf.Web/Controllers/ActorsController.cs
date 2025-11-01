using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMovieShelf.Domain.DomainModels;
using MyMovieShelf.Repository;
using MyMovieShelf.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyMovieShelf.Web.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorService _actorService;
        private readonly IMovieService _movieService;
        private readonly IDataFetchService _dataFetchService;

        public ActorsController(IActorService actorService, IMovieService movieService,IDataFetchService dataFetchService)
        {
            _actorService = actorService;
            _movieService = movieService;
            _dataFetchService = dataFetchService;
        }

        // GET: Actors/Details/5
        public async Task<IActionResult> Details(int tmdbId)
        {
            var actor = await _dataFetchService.FetchActorDetails(tmdbId);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["Movies"] = _movieService.GetMoviesByActor(actor.Id, userId);

            return View(actor);
        }
    }
}
