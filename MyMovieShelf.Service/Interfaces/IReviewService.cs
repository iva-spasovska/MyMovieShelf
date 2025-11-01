using MyMovieShelf.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieShelf.Service.Interfaces
{
    public interface IReviewService
    {
        List<Review> GetAll();
        Review? GetById(Guid Id);
        Review Create(Guid movieId, string userId, int rating, string comment);
        Review Update(Guid reviewId, int rating, string comment);
        Review DeleteById(Guid id);

        //Review AddReview(Guid movieId, string userId, int rating, string comment);
        //void UpdateReview(Guid reviewId, int rating, string comment);
        List<Review> GetReviewsForMovie(Guid movieId);
        List<Review> GetReviewsByUser(string userId);
        //Review DeleteReview(Guid reviewId);
    }
}
