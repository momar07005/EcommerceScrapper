using Application.Data.DTO;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CollectorService : ICollectorService
    {
        private IScrapper _scrapper;

        public CollectorService(IScrapper scrapper)
        {
            _scrapper = scrapper ?? throw new ArgumentNullException(nameof(scrapper));
        }

        public Task<List<ResponseDTO>> CollectMultipleProductsReviews(CollectReviewsBulkRequestDTO bulkRequestDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO> CollectSingleProductReviews(CollectReviewsSingleRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }
    }
}
