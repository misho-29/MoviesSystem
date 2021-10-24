using Microsoft.Extensions.Hosting;
using MoviesSystem.NotificationSender.Worker.Models;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoviesSystem.NotificationSender.Worker.Scheduler
{
    class GeneralScheduler : IHostedService
    {
        public IScheduler Scheduler { get; set; }
        private readonly IJobFactory jobFactory;
        private readonly List<JobMetadata> jobMetadatas;
        private readonly ISchedulerFactory schedulerFactory;

        public GeneralScheduler(ISchedulerFactory schedulerFactory, List<JobMetadata> jobMetadatas, IJobFactory jobFactory)
        {
            this.jobFactory = jobFactory;
            this.schedulerFactory = schedulerFactory;
            this.jobMetadatas = jobMetadatas;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //Creating Schdeular
            Scheduler = await schedulerFactory.GetScheduler();
            Scheduler.JobFactory = jobFactory;
            
            jobMetadatas?.ForEach(jobMetadata =>
            {
                //Create Job
                IJobDetail jobDetail = CreateJob(jobMetadata);
                //Create trigger
                ITrigger trigger = CreateTrigger(jobMetadata);
                //Schedule Job
                Scheduler.ScheduleJob(jobDetail, trigger, cancellationToken).GetAwaiter();
                //Start The Schedular
            });
            await Scheduler.Start(cancellationToken);
        }

        private ITrigger CreateTrigger(JobMetadata jobMetadata)
        {
            return TriggerBuilder.Create()
                .WithIdentity(jobMetadata.JobId.ToString())
                .WithCronSchedule(jobMetadata.CronExpression)
                .WithDescription(jobMetadata.JobName)
                .Build();
        }

        private IJobDetail CreateJob(JobMetadata jobMetadata)
        {
            return JobBuilder.Create(jobMetadata.JobType)
                .WithIdentity(jobMetadata.JobId.ToString())
                .WithDescription(jobMetadata.JobName)
                .Build();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler.Shutdown();
        }
    }
}
