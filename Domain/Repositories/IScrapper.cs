using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repositories
{
    public interface IScrapper
    {
        public string Extract(Request request);

        public Response Transform(string content);
    }
}
