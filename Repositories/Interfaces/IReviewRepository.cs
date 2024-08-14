using LaptopCenter.DTO;
using LaptopCenter.Models;

namespace LaptopCenter.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        List<Review> GetReviewsByProductId(int productId);
        List<Review> GetReviewByProductId(int productId, string userId);
        string SaveReview(Review review);
        void UpdateReview(Review review);
        void RemoveReview(Review review);
    }
}
