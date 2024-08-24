using LaptopCenter.Data;
using LaptopCenter.DTO;
using LaptopCenter.Models;
using LaptopCenter.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace LaptopCenter.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Review> GetReviewByProductId(int productId, string userId)
        {
            throw new NotImplementedException();
        }

        public List<Review> GetReviewsByProductId(int productId)
        {
            return _context.Reviews
                .Include(r => r.Order)
                .Include(r => r.Product)
                .Where(r => r.ProductId == productId)
                .ToList();
        }

        public string SaveReview(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return "Save successfully";
        }

        public void UpdateReview(Review review)
        {
            _context.Reviews.Update(review);
            _context.SaveChanges();
        }

        public void RemoveReview(Review review)
        {
            _context.Reviews.Remove(review);
            _context.SaveChanges();
        }

        public List<Review> GetReviews()
        {
            return _context.Reviews.Include(r => r.Product).ToList();
        }

        public Review GetReviewById(int reviewId)
        {
            return _context.Reviews
                .Include(r => r.Product)
                .FirstOrDefault(m => m.Id == reviewId);
        }
    }
}
