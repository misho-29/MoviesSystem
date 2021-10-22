using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MoviesSystem.App.EmailService;
using MoviesSystem.App.NotificationManager;
using MoviesSystem.Domain.Models.Validators;
using MoviesSystem.Domain.Repositories;
using MoviesSystem.Domain.Services;
using MoviesSystem.ExternalService;
using MoviesSystem.Infrastructure.Mappers;
using MoviesSystem.Infrastructure.Repositories;
using MoviesSystem.Infrastructure.Services;
using MoviesSystem.Infrastructure.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesSystem.App.API
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
            services.AddControllers()
                .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<GetMovieByTitleRequestValidator>());

            services.AddDbContext<MoviesDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            var emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSender, EmailSender>();

            services.AddScoped<INotificationManager, NotificationManager.NotificationManager>()
                .AddScoped<HtmlGenerator>();

            services.AddScoped<IWatchlistService, WatchlistService>()
                .AddScoped<IWatchlistRepository, WatchlistRepository>();

            services.AddSingleton<IMovieApiService, MovieApiService>();
            services.AddHttpClient("MoviesApi", options =>
            {
                options.BaseAddress = new Uri(Configuration.GetSection("MoviesApiService")["Url"]);
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MoviesSystem.App.API", Version = "v1" });
                c.DescribeAllParametersInCamelCase();
            });

            services.AddAutoMapper(typeof(WatchlistProfile));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MoviesSystem.App.API v1"));
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
