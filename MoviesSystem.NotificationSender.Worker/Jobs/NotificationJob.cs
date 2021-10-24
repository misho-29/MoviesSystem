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
        private readonly ILogger<NotificationJob> _logger;

        public NotificationJob(ILogger<NotificationJob> logger)
        {
            _logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("1");
            return Task.CompletedTask;
        }
    }
}
