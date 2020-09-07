using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Data.DTO;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private ICollectorService _collectorService { get; set; }

        public ReviewsController(ICollectorService collectorService)
        {
            _collectorService = collectorService ?? throw new ArgumentNullException(nameof(collectorService));
        }

        // POST: api/<ReviewsController>
        //[HttpPost]
        //public ResponseDTO Post([FromBody] CollectReviewsSingleRequestDTO collectReviewsRequest)
        //{
        //    ResponseDTO response = new ResponseDTO();
        //    if (ModelState.IsValid)
        //    {
        //        response = _collectorService.CollectSingleProductReviews(collectReviewsRequest).Result;
        //    }
        //    return response;
        //}

        // POST: api/<ReviewsController>
        [HttpPost]
        public List<ResponseDTO> Post([FromBody] CollectReviewsBulkRequestDTO collectReviewsRequest)
        {
            List<ResponseDTO> responses = new List<ResponseDTO>();
            if (ModelState.IsValid)
            {
                responses = _collectorService.CollectMultipleProductsReviews(collectReviewsRequest).Result;
            }
            return responses;
        }
    }
}
