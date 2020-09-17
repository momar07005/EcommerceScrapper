using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Parser;
using Microsoft.Extensions.Configuration;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using AngleSharp.Dom;
using System.Linq;
using Domain.Enumerations;
using System.Net;
using System.Globalization;

namespace Infrastructure.Persistence.Repositories
{
    public class AmazonScrapper : IScrapper
    {
        private readonly IConfiguration _configuration;

        public AmazonScrapper(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<object> Extract(string request)
        {
            
            var config = Configuration.Default.WithDefaultLoader();
            
            var context = BrowsingContext.New(config);
           
            var document = await context.OpenAsync(request);

            return document;
        }

        public async Task<Response> Get(Request request, uint pageNumber)
        {
            string httpRequest = ToHttpRequest(request, pageNumber);

            IDocument document = await Extract(httpRequest) as IDocument;

            if(document.StatusCode != HttpStatusCode.OK)
            {
                return new Response();
            }

            Response response = Transform(request, document);

            return response;
        }

        public Response Transform(Request request, object content)
        {
            Response response = new Response();
            IDocument document = content as IDocument;
            var reviewElements = document.QuerySelectorAll("*[data-hook='review']");

            foreach (var reviewElement in reviewElements)
            {
                try
                {
                    Review review = new Review
                    {
                        ReviewId = reviewElement.Id,
                        Title = getReviewTitle(reviewElement),
                        Content = getReviewContent(reviewElement),
                        Date = getReviewDate(reviewElement),
                        Rate = getReviewRate(reviewElement),
                        ProductID = request.ProductId
                    };

                    response.Reviews.Add(review);
                }
                catch (Exception)
                {
                    continue;
                }
            }

            SetResponseStatus(response, reviewElements);

            return response;
        }

        private  void SetResponseStatus(Response response, IHtmlCollection<IElement> reviewElements)
        {
            if (response.Reviews.Count == reviewElements.Count() && reviewElements.Any())
            {
                response.ResponseStatus = ResponseStatusEnum.Success;
            }
            else
            {
                response.ResponseStatus = response.Reviews.Any() ? ResponseStatusEnum.PartialSuccess : ResponseStatusEnum.Failure;
            }
        }

        public async Task<int> GetTotalReviewsNumber(Request request)
        {
            string httpRequest = ToHttpRequest(request);

            IDocument document = await Extract(httpRequest) as IDocument;

            if (document.StatusCode != HttpStatusCode.OK)
            {
                return -1;
            }

            return GetTotalReviewsNumber(document);
        }

        private string ToHttpRequest(Request request, uint pageNumber)
        {
            string requestFormat = _configuration[Constant.AmazonBaseAdressConfigName] + _configuration[Constant.AmazonProductReviewsUrlFormatConfigName];

            return string.Format(requestFormat, request.ProductId, pageNumber);
        }

        private string ToHttpRequest(Request request)
        {
            return ToHttpRequest(request, 1);
        }

        private int GetTotalReviewsNumber(IDocument document)
        {
            try
            {
                string reviewCountInfoElement = document.QuerySelector("*[data-hook='cr-filter-info-review-rating-count']").Text().Replace("\n", "").Trim();
                int reviewsCountStartIndex = reviewCountInfoElement.IndexOf("| ") + 2;
                string totalReviewsNumberString = reviewCountInfoElement.Substring(reviewsCountStartIndex).Split(" ").FirstOrDefault();
                return int.Parse(totalReviewsNumberString, NumberStyles.AllowThousands);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private DateTime getReviewDate(IElement element)
        {
            string reviewDateElementContent = element.QuerySelector("*[data-hook='review-date']").Text().Replace("\n", "").Trim();
            int dateStartIndex = reviewDateElementContent.IndexOf("on ") + 3;
            string dateString = reviewDateElementContent.Substring(dateStartIndex);
            return DateTime.Parse(dateString);
        }

        private string getReviewTitle(IElement element)
        {
            return element.QuerySelector("*[data-hook='review-title']").Text().Replace("\n", "").Trim();
        }

        private string getReviewContent(IElement element)
        {
            return element.QuerySelector("*[data-hook='review-body']").Text().Replace("\n", "").Trim();
        }

        private decimal getReviewRate(IElement element)
        {
            string reviewRateElementContent = element.QuerySelector("*[data-hook='review-star-rating']").Text().Replace("\n", "").Trim();
            return decimal.Parse(reviewRateElementContent.Split(" ").FirstOrDefault());
        }
    }
}
