using Application.Data.Assemblers;
using Application.Data.DTO;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Domain.Enumerations;

namespace Application.Services
{
    public class CollectorService : ICollectorService
    {
        private IScrapper _scrapper;

        private IReviewRepository _reviewRepository;

        private IRequestRepository _requestRepository;

        public CollectorService(IScrapper scrapper, IReviewRepository reviewRepository, IRequestRepository requestRepository)
        {
            _scrapper = scrapper ?? throw new ArgumentNullException(nameof(scrapper));
            _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
            _requestRepository = requestRepository ?? throw new ArgumentNullException(nameof(requestRepository));
        }


        public List<ResponseDTO> CollectMultipleProductsReviews(CollectReviewsBulkRequestDTO bulkRequestDTO)
        {
            List<ResponseDTO> responseDTOs = new List<ResponseDTO>();

            List<CollectReviewsSingleRequestDTO> singleRequestDTOs = bulkRequestDTO.ToSingRequests();

            foreach (CollectReviewsSingleRequestDTO request in singleRequestDTOs)
            {
                ResponseDTO responseDTO = CollectSingleProductReviews(request);

                responseDTOs.Add(responseDTO);
            }

            return responseDTOs;
        }


        public ResponseDTO CollectSingleProductReviews(CollectReviewsSingleRequestDTO requestDTO)
        {
            Request request = _requestRepository.Add(requestDTO.ToRequest());

            int numberOfNeededRequests = GetNumberOfNeededRequests(requestDTO);

            if (numberOfNeededRequests <= 0)
            {
                return new ResponseDTO();
            }

            List<Response> responses = GetResponses(requestDTO, numberOfNeededRequests);

            List<Review> reviews = responses.Where(response => response.ResponseStatus != ResponseStatusEnum.Failure)
                                                          .SelectMany(response => response.Reviews).ToList();

            _reviewRepository.Add(reviews);

            ResponseDTO resultResponse = MergeResponses(responses).ToResponseDTO(requestDTO.ProductId);

            request.Status = resultResponse.ResponseStatus.ToRequestStatusEnum();

            _requestRepository.Update(request);

            return resultResponse;
        }

        
        private List<Response> GetResponses(CollectReviewsSingleRequestDTO requestDTO, int numberOfNeededRequests)
        {
            List<Response> responses = new List<Response>();

            List<Action> getPartialResponseActions = new List<Action>();

            for (uint i = 1; i <= numberOfNeededRequests; i++)
            {
                var pageNumber = i;

                getPartialResponseActions.Add(async () =>
                {
                    Response partialResponse = await _scrapper.Get(requestDTO.ToRequest(), pageNumber);

                    responses.Add(partialResponse);
                });

            }

            var options = new ParallelOptions { MaxDegreeOfParallelism = 3 };

            Parallel.Invoke(options, getPartialResponseActions.ToArray());

            return responses;
        }


        private int GetNumberOfNeededRequests(CollectReviewsSingleRequestDTO requestDTO)
        {
            int totalReviewsNumber = _scrapper.GetTotalReviewsNumber(requestDTO.ToRequest()).Result;

            if(totalReviewsNumber < 0)
            {
                return -1;
            }

            int numberOfReviewsToReturn = (int)Math.Min(totalReviewsNumber, requestDTO.NumberOfReviews);

            int numberOfNeededRequests = numberOfReviewsToReturn / 10;

            int numberOfReviewsInLastRequest = numberOfReviewsToReturn % 10;

            if (numberOfReviewsInLastRequest > 0)
            {
                numberOfNeededRequests++;
            }

            return numberOfNeededRequests;
        }


        private Response MergeResponses(List<Response> responses)
        {
            Response response = new Response();

            Response firstNonNullResponse = responses.FirstOrDefault();

            if (firstNonNullResponse != null)
            {
                response.Reviews = responses.Where(response => response.ResponseStatus != ResponseStatusEnum.Failure)
                                        .SelectMany(response => response.Reviews).ToList();
                response.Date = DateTime.Now;

                response.ResponseStatus = responses.Select(response => response.ResponseStatus).Distinct().Count() > 1 ?
                                          ResponseStatusEnum.PartialSuccess :
                                          firstNonNullResponse.ResponseStatus;
            }

            return response;
        }
    }
}
