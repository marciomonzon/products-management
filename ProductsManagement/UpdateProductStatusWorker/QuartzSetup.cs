using Quartz;
using UpdateProductStatusWorker.Jobs;

namespace UpdateProductStatusWorker
{
    public static class QuartzSetup
    {
        public static IServiceCollection AddQuartzJobs(this IServiceCollection services)
        {
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                var jobKey = new JobKey("UpdateProductStatusJob");

                q.AddJob<UpdateProductStatusJob>(opts => opts.WithIdentity(jobKey));

                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("UpdateProductStatusTrigger")
                    .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(5, 30))
                );
            });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            return services;
        }
    }
}
