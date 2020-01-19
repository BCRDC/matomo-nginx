
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Host.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace Host.Extensions
{
    public static class IApplicationBuilderExtension
    {
        public static void UseCustomizedRule(this IApplicationBuilder app)
        {
            using (StreamReader iisUrlRewriteStreamReader = File.OpenText("IISUrlRewrite.xml"))
            {
                var options = new RewriteOptions()
                    .AddIISUrlRewrite(iisUrlRewriteStreamReader);
                app.UseRewriter(options);
            }
        }



        public static void UseProxy(this IApplicationBuilder app, ProxySetting setting)
        {
            var uri = new Uri(setting.Url);
            app.RunProxy(uri);
            //    app.RunProxy(new ProxyOptions
            //    {
            //        Scheme = uri.Scheme,
            //        Host = uri.Host,
            //       // Port = uri.Port.ToString(),
            //        BackChannelMessageHandler =  new HttpClientHandler {
            //            AllowAutoRedirect = false,
            //            UseCookies = false,
            //            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            //}
            //    });

        }
    }
}
