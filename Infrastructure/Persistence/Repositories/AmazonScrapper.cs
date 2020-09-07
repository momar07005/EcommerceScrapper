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
            // Load default configuration
            var config = Configuration.Default.WithDefaultLoader();
            // Create a new browsing context
            var context = BrowsingContext.New(config);
            // This is where the HTTP request happens, returns <IDocument> that // we can query later
            var document = await context.OpenAsync(request);
            // Log the data to the console

            return document;
        }

        public async Task<Response> Get(Request request, uint pageNumber)
        {
            string httpRequest = ToHttpRequest(request, pageNumber);
            IDocument document = await Extract(httpRequest) as IDocument;
            Response response = await Transform(document);
            return response;
        }

        public async Task<Response> Transform(object content)
        {
            Response response = new Response();
            IDocument document = content as IDocument;
            var reviewElements = document.QuerySelectorAll("*[data-hook='review']");

            foreach(var reviewElement in reviewElements)
            {
                Review review = new Review();
                review.Title = getReviewTitle(reviewElement);
                review.Content = getReviewContent(reviewElement);
                review.Date = getReviewDate(reviewElement);
                review.Rate = getReviewRate(reviewElement);
                response.Reviews.Add(review);
            }

            response.ResponseStatus = ResponseStatusEnum.Success;

            return response;
        }

        private string ToHttpRequest(Request request, uint pageNumber)
        {
            string requestFormat = _configuration[Constant.AmazonBaseAdressConfigName] + _configuration[Constant.AmazonProductReviewsUrlFormatConfigName];

            return string.Format(requestFormat, request.ProductId, pageNumber);
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
