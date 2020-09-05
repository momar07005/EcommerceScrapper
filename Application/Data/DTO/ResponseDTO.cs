using Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Data.DTO
{
    public class ResponseDTO
    {
        public ResponseStatusEnum ResponseStatus { get; set; }

        public List<ProductReviewDTO> ProductReviews { get; set; }
    }
}
