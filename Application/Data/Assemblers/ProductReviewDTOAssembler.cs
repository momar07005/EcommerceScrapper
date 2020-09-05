using Application.Data.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Application.Data.Assemblers
{
    public static class ProductReviewDTOAssembler
    {
        public static ProductReviewDTO ToProductReviewDTO(this Review review, string productId)
        {
            if(review != null)
            {
                return new ProductReviewDTO
                {
                    Title = review.Title,
                    Content = review.Content,
                    Rate = review.Rate,
                    ProductId = productId,
                    Date = review.Date
                };
            }
            else
            {
                return new ProductReviewDTO { };
            }
        }

        public static List<ProductReviewDTO> ToProductReviewDTOList(this List<Review> reviews, string productId)
        {
            List<ProductReviewDTO> productReviewDTOs = new List<ProductReviewDTO>();

            if (reviews != null && reviews.Any())
            {
                productReviewDTOs = reviews.Select(review => review.ToProductReviewDTO(productId)).ToList();
            }

            return productReviewDTOs;
        }
    }
}
