using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repositories
{
    public interface IRequestRepository
    {
        public Request Add(Request request);

        public List<Request> GetAllRequest();

        List<Request> GetRequestsByProductId(string productId);

        public Request Update(Request request);
    }
}
