using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Infrastructure.Persistence.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private ScrappingContext _scrappingContext;

        public ReviewRepository(ScrappingContext scrappingContext)
        {
            _scrappingContext = scrappingContext ?? throw new ArgumentNullException(nameof(scrappingContext));
        }

        public int Add(List<Review> reviews)
        {
            if (reviews != null && reviews.Any())
            {
                foreach (Review review in reviews)
                {
                    bool isReviewAlreadyRegistred = _scrappingContext.Reviews.Any(re => re.ReviewId == review.ReviewId);
                    
                    if(isReviewAlreadyRegistred == false)
                    {
                        _scrappingContext.Reviews.AddRange(review);
                    }                     
                }

                return _scrappingContext.SaveChanges();
            }

            return -1;
        }

        public List<Review> GetAllReviews()
        {
            return _scrappingContext.Reviews.ToList();
        }

        public List<Review> GetReviewsByProductId(string productId)
        {
            if(productId != null)
            {
                return _scrappingContext.Reviews.Where(review => review.ProductID == productId).ToList();
            }
            else
            {
                return new List<Review>();
            }
            
        }
    }
}
