using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Persistence.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly ScrappingContext _scrappingContext;

        public RequestRepository(ScrappingContext scrappingContext)
        {
            _scrappingContext = scrappingContext ?? throw new ArgumentNullException(nameof(scrappingContext));
        }

        public Request Add(Request request)
        {
           if(request != null)
           {
                request = _scrappingContext.Requests.Add(request).Entity;
                _scrappingContext.SaveChanges();
           }

           return request;
        }

        public List<Request> GetAllRequest()
        {
            return _scrappingContext.Requests.ToList() ;
        }

        public List<Request> GetRequestsByProductId(string productId)
        {
            if(productId != null)
            {
                return _scrappingContext.Requests.Where(request => request.ProductId == productId).ToList();
            }
            else
            {
                return new List<Request>();
            }
            
        }

        public Request Update(Request request)
        {
            if (request != null && request.Id > 0)
            {
                request = _scrappingContext.Requests.Attach(request).Entity;
                _scrappingContext.SaveChanges();
            }

            return request;
        }
    }
}
