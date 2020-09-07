using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IScrapper
    {
        public Task<string> Extract(string request);

        public Task<Response> Transform(string content);

        public Task<Response> Get(Request request, uint pageNumber);
    }
}
