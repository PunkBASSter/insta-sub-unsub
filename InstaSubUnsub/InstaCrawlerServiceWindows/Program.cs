using InstaPersistence;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostBuilderContext, services) =>
    {
        var config = hostBuilderContext.Configuration;

        new InstaCrawlerApp.ContainerModule().Register(services, config);
        new InstaPersistence.ContainerModule().Register(services, config);
        new SeleniumUtils.ContainerModule().Register(services, config);
        new InstaJobs.ContainerModule().Register(services, config); //Already contains Quartz HostService registration.
        //services.AddHostedService<ScopedWorker>();
    })
    .Build();

using var scope = host.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<InstaDbContext>();
db.Database.Migrate();

host.Run();
