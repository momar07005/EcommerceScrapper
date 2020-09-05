using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Request : Entity<Request>
    {
        public List<string> ProductIds { get; set; }

        public uint NumberOfReviews { get; set; }

        public Response Response { get; set; }

        public DateTime Date { get; set; }

        protected override bool EqualsCore(Request obj)
        {
            throw new NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }
    }
}
