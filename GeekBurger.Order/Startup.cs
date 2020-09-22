using AutoMapper;
using GeekBurger.Order.Extension;
using GeekBurger.Order.Repository;
using GeekBurger.Order.Repository.Interfaces;
using GeekBurger.Order.Service;
using GeekBurger.Order.Service.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeekBurger.Order
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("appSettings.json").Build();
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                //.SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                ;
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
            services.AddAutoMapper();
            services.AddScoped<IReceiveMessagesFactory, ReceiveMessagesFactory>();

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();

            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IServiceBusService, ServiceBusService>();

            services.AddDbContext<OrderDbContext>(o => o.UseInMemoryDatabase("geekburger-orders"));

            //services.AddSingleton<IOrderChangedService, OrderChangedService>();
            var mvcCoreBuilder = services.AddMvcCore();

            mvcCoreBuilder
                .AddFormatterMappings()
                .AddJsonFormatters();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, OrderDbContext orderDbContext )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseCors("AllowAll");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(@"/swagger/v1/swagger.json", "GeekBurguerOrder");
            });

            app.ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetService<IReceiveMessagesFactory>();

            app.ApplicationServices.CreateScope()
                .ServiceProvider.
                GetService<IServiceBusService>()
                .CriarTopico();

            orderDbContext.Seed();

        }
    }
}