using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Data.DTO
{
    public class CollectReviewsRequestDTO 
    {
        public uint NumberOfReviews { get; set; }

        public DateTime Date { get; set; }
    }
}
