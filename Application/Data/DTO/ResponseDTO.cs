using Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Data.DTO
{
    public class ResponseDTO
    {
        public ResponseStatusEnum ResponseStatus { get; set; } = ResponseStatusEnum.Failure;

        public List<ProductReviewDTO> ProductReviews { get; set; } = new List<ProductReviewDTO>();
    }
}
