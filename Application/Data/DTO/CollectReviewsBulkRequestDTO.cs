using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Data.DTO
{
    public class CollectReviewsBulkRequestDTO : CollectReviewsRequestDTO
    {
        public List<string> ProductIds { get; set; }
    }
}
