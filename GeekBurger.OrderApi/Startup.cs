using GeekBurger.OrderApi.Extension;
using GeekBurger.OrderApi.Repository;
using GeekBurger.OrderApi.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace GeekBurger.OrderApi
{
    public class Startup
    {
        public static IConfiguration Configuration;
        public IHostingEnvironment HostingEnvironment;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "GeekBurguerOrder",
                    Description = "GeekBurguer Order Api"
                });
            });

            services.AddCors();
            services.AddDbContext<OrderDbContext>(o => o.UseInMemoryDatabase("geekburger-orders-v2"));
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IOrderService, OrderService>();
        }

        public void Configure(IApplicationBuilder app, OrderDbContext orderDbContext)
        {
            if (HostingEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseCors();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint(@"/swagger/v1/swagger.json", "GeekBurguerOrder");
            });

            using (var serviceScope = app
                .ApplicationServices
                .GetService<IServiceScopeFactory>()
                .CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<OrderDbContext>();
                //context.Database.Migrate();
                context.Database.EnsureCreated();
            }

            orderDbContext.Seed();

        }
    }
}