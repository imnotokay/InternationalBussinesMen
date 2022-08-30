using InternationalBusinessMen.ApplicationCore;
using InternationalBusinessMen.ApplicationCore.Interfaces;
using InternationalBusinessMen.ApplicationCore.Providers;
using InternationalBusinessMen.Extensions;
using InternationalBusinessMen.Services.Interfaces;
using InternationalBusinessMen.Services.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InternationalBusinessMen
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
            services.AddControllers();
            
            services.AddSingleton<ILoggerService, LoggerProvider>();
            services.AddTransient(typeof(IDownloadJsonService<>), typeof(DownloadJsonProvider<>));
            services.AddTransient(typeof(IStorageJsonLocal<>), typeof(StorageJsonLocalProvider<>));
            services.AddSingleton< ITransactionsService, TransactionsProvider>();

            services.AddSingleton<IInternationalBusinessService, InternationalBusinessProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
                        
            app.UseLoggingMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
