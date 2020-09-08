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

        public CollectorService(IScrapper scrapper)
        {
            _scrapper = scrapper ?? throw new ArgumentNullException(nameof(scrapper));
        }

        public async Task<List<ResponseDTO>> CollectMultipleProductsReviews(CollectReviewsBulkRequestDTO bulkRequestDTO)
        {
            List<ResponseDTO> responseDTOs = new List<ResponseDTO>();

            List<CollectReviewsSingleRequestDTO> singleRequestDTOs = bulkRequestDTO.ToSingRequests();

            foreach (CollectReviewsSingleRequestDTO request in singleRequestDTOs)
            {
                ResponseDTO responseDTO = await CollectSingleProductReviews(request);

                responseDTOs.Add(responseDTO);
            }

            return responseDTOs;
        }

        public async Task<ResponseDTO> CollectSingleProductReviews(CollectReviewsSingleRequestDTO requestDTO)
        {
            uint numberOfNeededRequests = GetNumberOfNeededRequests(requestDTO);

            List<Response> responses = new List<Response>();
            Response partialResponse;

            for (uint i = 1; i <= numberOfNeededRequests; i++)
            {
                partialResponse = await _scrapper.Get(requestDTO.ToRequest(), i);

                responses.Add(partialResponse);
            }

            return MergeResponses(responses).ToResponseDTO(requestDTO.ProductId);
        }

        private uint GetNumberOfNeededRequests(CollectReviewsSingleRequestDTO requestDTO)
        {
            uint totalReviewsNumber = _scrapper.GetTotalReviewsNumber(requestDTO.ToRequest()).Result;

            uint numberOfReviewsToReturn = Math.Min(totalReviewsNumber, requestDTO.NumberOfReviews);

            uint numberOfNeededRequests = numberOfReviewsToReturn / 10;

            uint numberOfReviewsInLastRequest = numberOfReviewsToReturn % 10;

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

            response.Reviews = responses.Where(response => response.ResponseStatus == ResponseStatusEnum.Success)
                                        .SelectMany(response => response.Reviews).ToList();
            response.Date = DateTime.Now;

            response.ResponseStatus = ResponseStatusEnum.Success;

            return response;
        }
    }
}
