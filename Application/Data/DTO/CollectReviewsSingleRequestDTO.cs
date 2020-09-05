using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Data.DTO
{
    public class CollectReviewsSingleRequestDTO : CollectReviewsRequestDTO
    {
        public string ProductId { get; set; }
    }
}
