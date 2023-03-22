using InstaCommon.Config.Jobs;
using InstaPersistence;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostBuilderContext, services) =>
    {
        var config = hostBuilderContext.Configuration;

        new InstaCrawlerApp.ContainerModule().Register(services, config);
        new InstaPersistence.ContainerModule().Register(services, config);
        new SeleniumUtils.ContainerModule().Register(services, config);
        new InstaJobs.ContainerModule().Register(services, config); //Already contains Quartz HostService registration.
        //services.AddHostedService<ScopedWorker>();

        var configurationRoot = config;

        var options =
            configurationRoot.GetSection(nameof(JobConfigBase))
                             .Get<JobConfigBase>();
    })
    .Build();

using var scope = host.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<InstaDbContext>();
db.Database.Migrate();

host.Run();
