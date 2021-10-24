using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MoviesSystem.App.NotificationManager;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSystem.NotificationSender.Worker.Jobs
{
    class NotificationJob : IJob
    {
        private readonly IServiceProvider _serviceProvider;

        public NotificationJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public Task Execute(IJobExecutionContext context)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var notificationManager = scope.ServiceProvider.GetService<INotificationManager>();
                notificationManager.NotifyAboutUnwatchedMovies();
            }
            return Task.CompletedTask;
        }
    }
}
