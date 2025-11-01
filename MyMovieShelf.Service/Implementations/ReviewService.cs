using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
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
    public class ReviewService : IReviewService
    {
        private readonly IRepository<Review> _repository;
        private readonly IMovieService _movieService;

        public ReviewService(IRepository<Review> repository, IMovieService movieService)
        {
            _repository = repository;
            _movieService = movieService;
        }

        public List<Review> GetAll()
        {
            return _repository.GetAll(selector: x => x).ToList();
        }

        public Review? GetById(Guid Id)
        {
            return _repository.Get(selector: x => x,
                predicate: x => x.Id == Id);
        }

        public Review Create(Guid movieId, string userId, int rating, string comment)
        {
            var movie = _movieService.GetById(movieId);

            var review = new Review()
            {
                MovieId = movieId,
                Movie = movie,
                UserId = userId,
                Rating = rating,
                Comment = comment,
                CreatedAt = DateTime.Now,
            };

            return _repository.Insert(review);
        }

        public Review Update(Guid reviewId, int rating, string comment)
        {
            var review = GetById(reviewId);
            review.Rating = rating;
            review.Comment = comment;

            return _repository.Update(review);
        }

        public Review DeleteById(Guid id)
        {
            var review = GetById(id);
            return _repository.Delete(review);
        }

        public List<Review> GetReviewsByUser(string userId)
        {
            return _repository.GetAll(
                selector: x => x,
                predicate: x => x.UserId == userId,
                include: x => x.Include(r => r.Movie)
            ).ToList();
        }

        public List<Review> GetReviewsForMovie(Guid movieId)
        {
            return _repository.GetAll(
                 selector: x => x,
                 predicate: x => x.MovieId == movieId,
                 include: x => x.Include(y => y.User)
             ).ToList();
        }
    }
}
