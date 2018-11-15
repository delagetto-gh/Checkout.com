using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.P1.BasketManagement.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Checkout.P1.BasketManagement.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<BasketApplicationService>();
            services.AddSingleton<IBasketApplicationService>(o => o.GetService<BasketApplicationService>());
            services.AddSingleton<IBootStrap>(o => o.GetService<BasketApplicationService>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.ApplicationServices.GetService<IBootStrap>().Start();
        }
    }
}
