using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Data.Assemblers;
using Application.Data.DTO;
using Application.Services;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private IReviewRepository _reviewRepository { get; set; }

        public ReviewsController(ICollectorService collectorService, IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
        }

        [HttpGet("{productId}")]
        public List<ProductReviewDTO> Get(string productId)
        {
            List<ProductReviewDTO> reviews = new List<ProductReviewDTO>();

            if (ModelState.IsValid)
            {
                reviews = _reviewRepository.GetReviewsByProductId(productId).ToProductReviewDTOList();
            }

            return reviews;
        }

        [HttpGet]
        public List<ProductReviewDTO> GetAllReviews()
        {
            List<ProductReviewDTO> reviews = new List<ProductReviewDTO>();

            if (ModelState.IsValid)
            {
                reviews = _reviewRepository.GetAllReviews().ToProductReviewDTOList();
            }

            return reviews;
        }
    }
}
