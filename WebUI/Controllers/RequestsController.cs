using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Data.Assemblers;
using Application.Data.DTO;
using Application.Services;
using Domain.Repositories;
using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private ICollectorService _collectorService { get; set; }

        private IRequestRepository _requestRepository { get; set; }

        public RequestsController(ICollectorService collectorService, IRequestRepository requestRepository)
        {
            _collectorService = collectorService ?? throw new ArgumentNullException(nameof(collectorService));
            _requestRepository = requestRepository ?? throw new ArgumentNullException(nameof(requestRepository));
        }

        [HttpPost]
        public List<ResponseDTO> Post([FromBody] CollectReviewsBulkRequestDTO collectReviewsRequest)
        {
            List<ResponseDTO> responses = new List<ResponseDTO>();

            if (ModelState.IsValid)
            {
                responses = _collectorService.CollectMultipleProductsReviews(collectReviewsRequest);
            }

            return responses;
        }

        [HttpGet("{productId}")]
        public List<CollectReviewsSingleRequestDTO> Get(string productId)
        {
            List<CollectReviewsSingleRequestDTO> requests = new List<CollectReviewsSingleRequestDTO>();

            if (ModelState.IsValid)
            {
                requests = _requestRepository.GetRequestsByProductId(productId).ToRequestDTOs();
            }

            return requests;
        }

        [HttpGet]
        public List<CollectReviewsSingleRequestDTO> GetAll()
        {
            List<CollectReviewsSingleRequestDTO> requests = new List<CollectReviewsSingleRequestDTO>();

            if (ModelState.IsValid)
            {
                requests = _requestRepository.GetAllRequest().ToRequestDTOs();
            }

            return requests;
        }
    }
}
