using CorePlusMongoDBApi.Interfaces;
using CorePlusMongoDBApi.Models;
using CorePlusMongoDBApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace CorePlusMongoDBApi
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
            //my mongo connection strings
            //1 . "ConnectionString": "mongodb://localhost:27017",
            //2. "ConnectionString": "mongodb+srv://shawn72:2950Cherry*30@cluster0.k12xz.mongodb.net/ecommercestore?retryWrites=true&w=majority",
 
            services.Configure<EcommerceDBSettings>(
                Configuration.GetSection(nameof(EcommerceDBSettings)));

            services.AddSingleton<IEcommerceDBSettings>(provider =>
                provider.GetRequiredService<IOptions<EcommerceDBSettings>>().Value);

            services.AddSingleton<ProductsService>(); //add products service
            services.AddSingleton<CategoryService>(); //add category service

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
