using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IScrapper
    {
        public Task<object> Extract(string request);

        public Task<Response> Transform(Request request, object content);

        public Task<Response> Get(Request request, uint pageNumber);

        public Task<uint> GetTotalReviewsNumber(Request request);
    }
}
