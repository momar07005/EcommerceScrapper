using Application.Data.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Data.Assemblers
{
    public static class ResponseDTOAssembler
    {
        public static ResponseDTO ToResponseDTO(this Response response, string productId)
        {
            if (response != null)
            {
                return new ResponseDTO
                {
                    ProductReviews = response.Reviews.ToProductReviewDTOList(productId),
                    ResponseStatus = response.ResponseStatus
                };
            }
            else
            {
                return new ResponseDTO();
            }
        }
    }
}
