using Microsoft.AspNetCore.Mvc;
using MyMovieShelf.Service.Implementations;
using MyMovieShelf.Service.Interfaces;
using MyMovieShelf.Web.Models;
using System.Diagnostics;

namespace MyMovieShelf.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataFetchService _dataFetchService;

        public HomeController(ILogger<HomeController> logger, IDataFetchService dataFetchService)
        {
            _logger = logger;
            _dataFetchService = dataFetchService;
        }

        public async Task<IActionResult> Index()
        {
            var featuredMovies = await _dataFetchService.GetPopularMovies();
            return View(featuredMovies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
