using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    public class ReviewsController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var reviews = _reviewService.GetReviewsByUser(userId);
            return View(reviews);
        }

        [Authorize]
        public IActionResult Create(Guid movieId)
        {
            var dto = new ReviewDTO { MovieId = movieId };
            return View(dto);
        }

        [HttpPost, Authorize]
        public IActionResult Create(ReviewDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _reviewService.Create(dto.MovieId, userId, dto.Rating, dto.Comment);

            return RedirectToAction("Details", "Shelves", new { id = dto.MovieId });
        }

        [Authorize]
        public IActionResult Edit(Guid id)
        {
            var review = _reviewService.GetById(id);
            if (review == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (review.UserId != userId) return Forbid();

            var dto = new ReviewDTO
            {
                Id = review.Id,
                MovieId = review.MovieId,
                Rating = review.Rating,
                Comment = review.Comment
            };

            return View(dto);
        }

        [HttpPost, Authorize]
        public IActionResult Edit(ReviewDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var review = _reviewService.GetById(dto.Id);
            if (review == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (review.UserId != userId) return Forbid();

            _reviewService.Update(dto.Id, dto.Rating, dto.Comment);

            return RedirectToAction("Details", "Shelves", new { id = dto.MovieId });
        }

        [HttpPost, Authorize]
        public IActionResult Delete(Guid id)
        {
            var review = _reviewService.GetById(id);
            if (review == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (review.UserId != userId) return Forbid();

            _reviewService.DeleteById(id);
            return RedirectToAction("Details", "Shelves", new { id = review.MovieId });
        }
    }
}
