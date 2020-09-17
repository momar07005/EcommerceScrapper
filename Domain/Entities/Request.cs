using Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Request : Entity<Request>
    {
        public string ProductId { get; set; }

        public uint NumberOfReviews { get; set; }

        public RequestStatusEnum Status { get; set; } = RequestStatusEnum.Failure;

        public DateTime Date { get; set; }

        protected override int GetHashCodeCore()
        {
            return Id.GetHashCode() ^ ProductId.GetHashCode();
        }
    }
}
