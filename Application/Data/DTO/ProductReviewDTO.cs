using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Data.DTO
{
    public class ProductReviewDTO
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public decimal Rate { get; set; }

        public DateTime Date { get; set; }

        public string ProductId { get; set; }
    }
}
