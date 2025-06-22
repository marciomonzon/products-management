using Quartz;
using UpdateProductStatusWorker.Jobs;

namespace UpdateProductStatusWorker
{
    public static class QuartzSetup
    {
        public static IServiceCollection AddQuartzJobs(this IServiceCollection services, IConfiguration configuration)
        {
            var hour = configuration.GetValue<int>("QuartzSettings:HourToStart");
            var time = configuration.GetValue<int>("QuartzSettings:TimeToStart");

            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                var jobKey = new JobKey("UpdateProductStatusJob");

                q.AddJob<UpdateProductStatusJob>(opts => opts.WithIdentity(jobKey));

                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("UpdateProductStatusTrigger")
                    .WithSchedule(CronScheduleBuilder
                                 .DailyAtHourAndMinute(hour, time))
                );
            });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            return services;
        }
    }
}
