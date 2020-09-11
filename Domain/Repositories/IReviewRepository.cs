using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repositories
{
    public interface IReviewRepository
    {
        public int Add(List<Review> reviews);

        public List<Review> GetReviewsByProductId(string productId);

        public List<Review> GetAllReviews();
    }
}
