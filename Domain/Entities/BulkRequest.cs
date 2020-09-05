using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class BulkRequest : Entity<BulkRequest>
    {
        public List<Request> Requests { get; set; }

        public DateTime Date { get; set; }

        protected override bool EqualsCore(BulkRequest obj)
        {
            throw new NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }
    }
}
