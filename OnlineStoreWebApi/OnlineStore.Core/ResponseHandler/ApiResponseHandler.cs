using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace OnlineStore.Core.ResponseHandler
{
    public class ApiResponseHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return response;
            }

            if (request.RequestUri.ToString().IndexOf("/download") != -1)
            {
                return response;
            }

            return BuildApiResponse(request, response);

        }

        private HttpResponseMessage BuildApiResponse(HttpRequestMessage request, HttpResponseMessage response)
        {
            object content = null;
            string errorMessage = null;
            int statusCode = 0;

            ValidateApiResponse(response, ref content, ref statusCode, ref errorMessage);

            var newResponse = CreateHttpResponseMessage(request, response, content, statusCode, errorMessage);

            foreach (var header in response.Headers)
            {
                newResponse.Headers.Add(header.Key, header.Value);
            }

            return newResponse;
        }

        private HttpResponseMessage CreateHttpResponseMessage<T>(HttpRequestMessage request, HttpResponseMessage response, T content, int statusCode, string errorMessage)
        {
            return request.CreateResponse(response.StatusCode, new ApiResponse<T>(statusCode, content, errorMessage));
        }

        private void ValidateApiResponse(HttpResponseMessage response, ref object content, ref int statusCode, ref string errorMessage)
        {
            statusCode = (int)response.StatusCode;
            if (response.TryGetContentValue(out content) && !response.IsSuccessStatusCode)
            {
                HttpError error = content as HttpError;

                if (error != null)
                {
                    content = null;

                    StringBuilder sb = new StringBuilder();

                    foreach (var item in error)
                    {
                        if (item.Key.Trim() != "StackTrace" && item.Key.Trim() != "ExceptionType")
                        {
                            sb.Append($"{item.Value}");
                            //sb.Append($"{item.Key} {item.Value}");
                        }
                    }

                    errorMessage = sb.ToString();
                    statusCode = 400;
                }
            }
        }
    }
}