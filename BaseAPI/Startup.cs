using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BaseAPI.Application;
using BaseAPI.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using AutoMapper;
using BaseAPI.Filter;
using BaseAPI.Application.Abstracts;

namespace BaseAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplictionContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)        
        {
            services.AddMvc();
            //TODO:Add Swagger
            var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "1.0 alpha",  //beta
                    Title = "Base API",
                    Description = "It's a back-end API",
                    TermsOfService = "None",
                });
                //Set the comments path for the swagger json and ui.
                var xmlPath = Path.Combine(basePath, "BaseAPI.xml");
                c.IncludeXmlComments(xmlPath);
            });
            services.AddAutoMapper();
            services.AddScoped<BaseAPI.Filter.ExceptionFilter>();
            services.AddDbContext<BaseContext>(options => options.UseMySql(Configuration.GetSection("SqlConnection")["Default"]));
            //TODO:use Microsoft DI
            //services.AddTransient(typeof(IOrderService), typeof(OrderService));
            //services.AddScoped(typeof(IUserService), typeof(UserSerive));

            //TODO:use Autofac
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterType<UserSerive>().As<IUserService>().InstancePerLifetimeScope();
        
            this.ApplictionContainer = builder.Build();
            return new AutofacServiceProvider(ApplictionContainer);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Base Api V1");
            });
            app.UseMvc();
        }
    }
}
