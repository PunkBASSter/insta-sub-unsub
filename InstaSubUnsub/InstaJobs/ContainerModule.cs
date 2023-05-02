using InstaCommon.Config.Jobs;
using InstaCrawlerApp.Jobs;
using InstaCrawlerApp.Scheduling;
using InstaInfrastructureAbstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace InstaJobs
{
    public class ContainerModule : IContainerModule
    {
        public void Register(IServiceCollection services, IConfiguration config)
        {
            services.AddTransient(typeof(QuartzJobWrapper<>));
            services.AddTransient(typeof(RandomDelayQuartzJobWrapper<>));
            services.AddTransient(typeof(MegaRandomQuartzJobWrapper<>));

            //services.AddTransient<RandomDelayQuartzJobWrapper<UserCrawler>>();
            //services.AddTransient<RandomDelayQuartzJobWrapper<UserFullDetailsProvider>>();
            //services.AddTransient<MegaRandomQuartzJobWrapper<Follower>>();
            //services.AddTransient<MegaRandomQuartzJobWrapper<Unfollower>>();
            //services.AddTransient<>

            //var quartzOptions = 
            //services.Configure<QuartzOptions>(() => configuration.GetSection("Quartz").Value);


            services.AddQuartz(q =>
            {
                q.SchedulerId = "Scheduler-Core";
                q.UseMicrosoftDependencyInjectionJobFactory();
                // or for scoped service support like EF Core DbContext
                // q.UseMicrosoftDependencyInjectionScopedJobFactory();

                // these are the defaults
                q.UseSimpleTypeLoader();
                q.UseInMemoryStore();
                q.UseDefaultThreadPool(tp =>
                {
                    //has to be one until AccountPool lock does not work correctly
                    tp.MaxConcurrency = 4; // 1 for each possible job
                });

#if DEBUG
                //q.ScheduleJob<QuartzJobWrapper<UserCrawler>>(trigger => trigger.WithIdentity(nameof(UserCrawler)).StartNow());
                //q.ScheduleJob<QuartzJobWrapper<UserFullDetailsProvider>>(trigger => trigger.WithIdentity(nameof(UserFullDetailsProvider)).StartNow());
                q.ScheduleJob<QuartzJobWrapper<Follower>>(trigger => trigger.WithIdentity(nameof(Follower)).StartNow());
                //q.ScheduleJob<QuartzJobWrapper<Unfollower>>(trigger => trigger.WithIdentity(nameof(Unfollower)).StartNow());
#endif

#if RELEASE
                var crawlerConfig = new UserCrawlerJobConfig(config);
                var crawlStartHour = crawlerConfig.WorkStartingHour ?? 3;
                var crawlEndHour = crawlStartHour + (crawlerConfig.WorkDurationHours ?? 16);
                q.ScheduleJob<RandomDelayQuartzJobWrapper<UserCrawler>>(trigger => trigger
                    .WithIdentity(nameof(UserCrawler))
                    .WithCronSchedule($"0 0 {crawlStartHour}-{crawlEndHour}/1 * * ?",
                        x => x.WithMisfireHandlingInstructionFireAndProceed())
                );
                
                var detailsProviderConf = new UserFullDetailsProviderJobConfig(config);
                var detailsStartHour = detailsProviderConf.WorkStartingHour ?? 1;
                var detailsEndHour = detailsStartHour + (detailsProviderConf.WorkDurationHours ?? 16);
                q.ScheduleJob<RandomDelayQuartzJobWrapper<UserFullDetailsProvider>>(trigger => trigger
                    .WithIdentity(nameof(UserFullDetailsProvider))
                    .WithCronSchedule($"0 0 {detailsStartHour}-{detailsEndHour}/1 * * ?", 
                        x => x.WithMisfireHandlingInstructionFireAndProceed())
                );

                var followerConf = new FollowerJobConfig(config);
                var followerStartHour = followerConf.WorkStartingHour ?? 10;
                var followerEndHour = followerStartHour + (followerConf.WorkDurationHours ?? 12);
                q.ScheduleJob<RandomDelayQuartzJobWrapper<Follower>>(trigger => trigger
                    .WithIdentity(nameof(Follower))
                    .WithCronSchedule($"0 0 {followerStartHour}-{followerEndHour}/2 * * ?",
                        x => x.WithMisfireHandlingInstructionFireAndProceed())
                );
                
                var unfollowerConf = new UnfollowerJobConfig(config);
                var unfollowerStartHour = unfollowerConf.WorkStartingHour ?? 10;
                var unfollowerEndHour = unfollowerStartHour + (unfollowerConf.WorkDurationHours ?? 12);
                q.ScheduleJob<RandomDelayQuartzJobWrapper<Unfollower>>(trigger => trigger
                    .WithIdentity(nameof(Unfollower))
                    .WithCronSchedule($"0 0 {unfollowerStartHour}-{unfollowerEndHour}/2 * * ?",
                        x => x.WithMisfireHandlingInstructionFireAndProceed())
                );

#endif

                /*
                 * # Will only run on odd days:
0 0 1-31/2 * * command

# Will only run on even days:
0 0 2-30/2 * * command
                 */

                // you can also configure individual jobs and triggers with code
                // this allows you to associated multiple triggers with same job
                // (if you want to have different job data map per trigger for example)
                //q.AddJob<QuartzJobWrapper<UserCrawler>>(j => j
                //    .StoreDurably() // we need to store durably if no trigger is associated
                //    .WithDescription("my awesome job")
                //);

                //// here's a known job for triggers
                //var jobKey = new JobKey("awesome job", "awesome group");
                //q.AddJob<ExampleJob>(jobKey, j => j
                //    .WithDescription("my awesome job")
                //);

                //q.AddTrigger(t => t
                //    .WithIdentity("Simple Trigger")
                //    .ForJob(jobKey)
                //    .StartNow()
                //    .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromSeconds(10)).RepeatForever())
                //    .WithDescription("my awesome simple trigger")
                //);

                //q.AddTrigger(t => t
                //    .WithIdentity("Cron Trigger")
                //    .ForJob(jobKey)
                //    .StartAt(DateBuilder.EvenSecondDate(DateTimeOffset.UtcNow.AddSeconds(3)))
                //    .WithCronSchedule("0/3 * * * * ?")
                //    .WithDescription("my awesome cron trigger")
                //);

                //// you can add calendars too (requires version 3.2)
                //const string calendarName = "myHolidayCalendar";
                //q.AddCalendar<HolidayCalendar>(
                //    name: calendarName,
                //    replace: true,
                //    updateTriggers: true,
                //    x => x.AddExcludedDate(new DateTime(2020, 5, 15))
                //);

                //q.AddTrigger(t => t
                //    .WithIdentity("Daily Trigger")
                //    .ForJob(jobKey)
                //    .StartAt(DateBuilder.EvenSecondDate(DateTimeOffset.UtcNow.AddSeconds(5)))
                //    .WithDailyTimeIntervalSchedule(x => x.WithInterval(10, IntervalUnit.Second))
                //    .WithDescription("my awesome daily time interval trigger")
                //    .ModifiedByCalendar(calendarName)
                //);

                //// also add XML configuration and poll it for changes
                //q.UseXmlSchedulingConfiguration(x =>
                //{
                //    x.Files = new[] { "~/quartz_jobs.config" };
                //    x.ScanInterval = TimeSpan.FromSeconds(2);
                //    x.FailOnFileNotFound = true;
                //    x.FailOnSchedulingError = true;
                //});

                //// convert time zones using converter that can handle Windows/Linux differences
                //q.UseTimeZoneConverter();

                //// auto-interrupt long-running job
                //q.UseJobAutoInterrupt(options =>
                //{
                //    // this is the default
                //    options.DefaultMaxRunTime = TimeSpan.FromMinutes(5);
                //});
                //q.ScheduleJob<SlowJob>(
                //    triggerConfigurator => triggerConfigurator
                //        .WithIdentity("slowJobTrigger")
                //        .StartNow()
                //        .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).RepeatForever()),
                //    jobConfigurator => jobConfigurator
                //        .WithIdentity("slowJob")
                //        .UsingJobData(JobInterruptMonitorPlugin.JobDataMapKeyAutoInterruptable, true)
                //        // allow only five seconds for this job, overriding default configuration
                //        .UsingJobData(JobInterruptMonitorPlugin.JobDataMapKeyMaxRunTime, TimeSpan.FromSeconds(5).TotalMilliseconds.ToString(CultureInfo.InvariantCulture)));

                //// add some listeners
                //q.AddSchedulerListener<SampleSchedulerListener>();
                //q.AddJobListener<SampleJobListener>(GroupMatcher<JobKey>.GroupEquals(jobKey.Group));
                //q.AddTriggerListener<SampleTriggerListener>();

                // example of persistent job store using JSON serializer as an example
                /*
                q.UsePersistentStore(s =>
                {
                    s.PerformSchemaValidation = true; // default
                    s.UseProperties = true; // preferred, but not default
                    s.RetryInterval = TimeSpan.FromSeconds(15);
                    s.UseSqlServer(sqlServer =>
                    {
                        sqlServer.ConnectionString = "some connection string";
                        // this is the default
                        sqlServer.TablePrefix = "QRTZ_";
                    });
                    s.UseJsonSerializer();
                    s.UseClustering(c =>
                    {
                        c.CheckinMisfireThreshold = TimeSpan.FromSeconds(20);
                        c.CheckinInterval = TimeSpan.FromSeconds(10);
                    });
                });
                */

            });

            // Quartz.Extensions.Hosting allows you to fire background service that handles scheduler lifecycle
            services.AddQuartzHostedService(options =>
            {
                // when shutting down we want jobs to complete gracefully
                options.WaitForJobsToComplete = true;
            });

        }
    }
}
