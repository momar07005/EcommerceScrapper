using Application.Data.Assemblers;
using Application.Data.DTO;
using Domain.Entities;
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

        public async Task<ResponseDTO> CollectSingleProductReviews(CollectReviewsSingleRequestDTO requestDTO)
        {
            Response response = await _scrapper.Get(requestDTO.ToRequest(), 1);

            return response.ToResponseDTO(requestDTO.ProductId);
        }
    }
}
