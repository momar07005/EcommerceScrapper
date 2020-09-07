using Application.Data.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Application.Data.Assemblers
{
    public static class CollectReviewsRequestDTOAssembler
    {
        public static Request ToRequest(this CollectReviewsSingleRequestDTO collectProductReviewsRequestDTO)
        {
            Request request = new Request();
            if (collectProductReviewsRequestDTO != null)
            {
                request = new Request
                {
                    NumberOfReviews = collectProductReviewsRequestDTO.NumberOfReviews,
                    Date = collectProductReviewsRequestDTO.Date,
                    ProductId = collectProductReviewsRequestDTO.ProductId
                };
            }
            return request;
        }

        public static BulkRequest ToBulkRequest(this CollectReviewsBulkRequestDTO collectReviewsBulkRequestDTO)
        {
            BulkRequest bulkRequest = new BulkRequest();

            if (collectReviewsBulkRequestDTO != null && collectReviewsBulkRequestDTO.ProductIds.Count > 1)
            {
               foreach(string productId in collectReviewsBulkRequestDTO.ProductIds)
               {
                    Request request = new Request
                    {
                        NumberOfReviews = collectReviewsBulkRequestDTO.NumberOfReviews,
                        Date = collectReviewsBulkRequestDTO.Date,
                        ProductId = productId
                    };

                    bulkRequest.Requests.Add(request);
                }
            }
            return bulkRequest;
        }

        public static List<CollectReviewsSingleRequestDTO> ToSingRequests(this CollectReviewsBulkRequestDTO collectProductReviewsBulkRequestDTO)
        {
            List<CollectReviewsSingleRequestDTO> singleRequests = new List<CollectReviewsSingleRequestDTO>();
            if (collectProductReviewsBulkRequestDTO != null)
            {
                singleRequests = collectProductReviewsBulkRequestDTO.ProductIds.Select(productId =>
                                                                                        new CollectReviewsSingleRequestDTO 
                                                                                        {
                                                                                            NumberOfReviews = collectProductReviewsBulkRequestDTO.NumberOfReviews,
                                                                                            ProductId = productId,
                                                                                            Date = collectProductReviewsBulkRequestDTO.Date
                                                                                        }
                                                                                       ).ToList();
            }
            return singleRequests;
        }
    }

}
