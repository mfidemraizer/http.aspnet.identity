namespace Http.AspNet.Identity
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class HttpRequestResponseHandler : DelegatingHandler
    {
        
        public HttpRequestResponseHandler(HttpMessageHandler innerHandler) : base(innerHandler)
        {
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Authentication.Lock.EnterReadLock();

            try
            {
                request.Headers.Authorization = Authentication.AuthenticationHeader;
            }
            finally
            {
                if (Authentication.Lock.IsReadLockHeld)
                    Authentication.Lock.ExitReadLock();
            }


            return base.SendAsync(request, cancellationToken);
        }
    }
}