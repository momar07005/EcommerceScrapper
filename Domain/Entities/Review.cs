using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Review : Entity<Review>
    {
        public string ReviewId { get; set; }

        public string ProductID { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public decimal Rate { get; set; }

        public DateTime Date { get; set; }

        protected override bool EqualsCore(Review obj)
        {
            throw new NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }
    }
}
