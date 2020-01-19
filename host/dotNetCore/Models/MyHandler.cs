using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Host.Models
{
    public class MyHandler : HttpMessageHandler
    {
        

        HttpClient _httpClient = null;

        HttpClientHandler handler = new HttpClientHandler();


        public MyHandler()
        {
            handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            _httpClient = new HttpClient(handler);
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // request.Headers.TryAddWithoutValidation("j", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString);
            using (var c = new HttpClient())
            {
                return await c.SendAsync(request);
            }
                

           // return null;
        }

        protected override void Dispose(bool isdisposee)
        {
            base.Dispose();
        }
    }
}
