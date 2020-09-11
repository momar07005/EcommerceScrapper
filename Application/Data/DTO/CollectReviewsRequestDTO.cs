using Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Data.DTO
{
    public class CollectReviewsRequestDTO 
    {
        public RequestStatusEnum Status { get; set; } = RequestStatusEnum.Failure;

        public uint NumberOfReviews { get; set; }

        public DateTime Date { get; set; }
    }
}
