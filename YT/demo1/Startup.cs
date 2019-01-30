using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo1.Bots;
using demo1.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder.BotFramework;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace demo1
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set the root path
            ContentRootPath = env.ContentRootPath;
        }
        // TRack the root path so taht is can be used to setup the app configuration
        public string ContentRootPath { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            var configuration = builder.Build();
            services.AddSingleton(configuration);

            services.AddBot<SimpleBot>(options => 
            {
                options.CredentialProvider = new ConfigurationCredentialProvider(configuration);
                options.Middleware.Add(new Middleware2());
                options.Middleware.Add(new PassthroughMiddleware());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();

            // Tell the app to use the Bot Framework
            app.UseBotFramework();
        }
    }
}
