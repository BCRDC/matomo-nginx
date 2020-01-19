using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using Host.Extensions;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.Extensions.Configuration;
using Host.Models;
using System.Net.Http;


namespace Host
{
    public class Startup
    {
        public  const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            // HttpClientHandler.DangerousAcceptAnyServerCertificateValidator


        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression();
            services.AddOptions();
            services.Configure<EWei>(Configuration.GetSection("EWei"));

            // services.Configure<ProxySettings>(Configuration.GetSection("Proxy"));
            // services.AddMvc();
            // ProxyServiceCollectionExtensions

            services.AddProxy(options =>
            {
                options.PrepareRequest = (originalRequest, message) =>
                {
                    message.Headers.Add("HTTP_X_FORWARDED_HOST", originalRequest.Host.Host);

                    message.Headers.Add("HTTP_CLIENT_IP", originalRequest.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());


                    message.Headers.Add("HTTP_X_FORWARDED_FOR", originalRequest.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
                    message.Headers.Add("HTTP_CF_CONNECTING_IP", originalRequest.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());


                    return Task.FromResult(0);
                };
                options.MessageHandler = new HttpClientHandler
                {
                    AllowAutoRedirect = false,
                    UseCookies = false,
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
            });
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var settings = new ProxySettings();
            Configuration.Bind("Proxy", settings);

            foreach (var setting in settings.Values)
            {
                app.UseProxy(setting);
            }

            if (env.IsDevelopment())
            {          
                app.UseDeveloperExceptionPage();         
            }

            //app.UseCustomizedRule();// rewrite
            //app.UseDefaultFiles();// wwwroot
            //app.UseStaticFiles();// wwwroot
            //app.UseResponseCompression();

            //app.UseMvc(); // webapi

        }
    }
}
