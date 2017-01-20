using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace JWTApi.Authorization.Tools
{
    public class CustomHttpActionResult<T> : IHttpActionResult
    {

        private T _content;

        private int _cacheTime;

        private HttpRequestMessage _request;

        private HttpStatusCode _statusCode;

        public CustomHttpActionResult(HttpRequestMessage request, T content, HttpStatusCode statusCode, int? cacheTime)
        {
            this._content = content;
            this._cacheTime = cacheTime ?? 0;
            this._request = request;
            this._statusCode = statusCode;
        }


        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = _request.CreateResponse(_statusCode, _content);
            if (_cacheTime > 0)
            {
                response.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    MaxAge = TimeSpan.FromSeconds(_cacheTime)
                };
            }

            return Task.FromResult(response);
        }
    }
}