using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoviesSystem.App.EmailService;
using MoviesSystem.App.NotificationManager;
using MoviesSystem.Domain.Repositories;
using MoviesSystem.Domain.Services;
using MoviesSystem.ExternalService;
using MoviesSystem.Infrastructure.Mappers;
using MoviesSystem.Infrastructure.Repositories;
using MoviesSystem.Infrastructure.Services;
using MoviesSystem.Infrastructure.Store;
using MoviesSystem.NotificationSender.Worker.JobFactory;
using MoviesSystem.NotificationSender.Worker.Jobs;
using MoviesSystem.NotificationSender.Worker.Models;
using MoviesSystem.NotificationSender.Worker.Scheduler;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesSystem.NotificationSender.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // Configure main items 

                    IConfiguration configuration = hostContext.Configuration;

                    services.AddDbContext<MoviesDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

                    var emailConfig = configuration
                        .GetSection("EmailConfiguration")
                        .Get<EmailConfiguration>();
                    services.AddSingleton(emailConfig);
                    services.AddScoped<IEmailSender, EmailSender>();

                    services.AddScoped<INotificationManager, NotificationManager>()
                        .AddScoped<HtmlGenerator>();

                    services.AddScoped<IWatchlistService, WatchlistService>()
                        .AddScoped<IWatchlistRepository, WatchlistRepository>();

                    services.AddSingleton<IMoviesApiService, MoviesApiService>();
                    services.AddHttpClient("MoviesApi", options =>
                    {
                        options.BaseAddress = new Uri(configuration.GetSection("MoviesApiService")["Url"]);
                    });

                    services.AddAutoMapper(typeof(WatchlistProfile));

                    // Configure jobs

                    services.AddSingleton<IJobFactory, MyJobFactory>();
                    services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

                    #region Adding JobType
                    services.AddSingleton<NotificationJob>();
                    #endregion

                    #region Adding Jobs 
                    List<JobMetadata> jobMetadatas = new List<JobMetadata>();
                    jobMetadatas.Add(new JobMetadata(Guid.NewGuid(), typeof(NotificationJob), "Notify Job", "0 30 19 ? * SUN *"));

                    services.AddSingleton(jobMetadatas);
                    #endregion

                    services.AddHostedService<GeneralScheduler>();
                });
    }
}
