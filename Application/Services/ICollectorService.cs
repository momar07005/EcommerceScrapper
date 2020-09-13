using Domain.Entities;
using Application.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Application.Data.DTO;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ICollectorService
    {
        public ResponseDTO CollectSingleProductReviews(CollectReviewsSingleRequestDTO requestDTO);

        public List<ResponseDTO> CollectMultipleProductsReviews(CollectReviewsBulkRequestDTO bulkRequestDTO);
        
    }
}
