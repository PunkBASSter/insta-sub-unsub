using InstaCrawlerServiceWindows;
using InstaPersistence;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostBuilderContext, services) =>
    {
        var config = hostBuilderContext.Configuration;

        services.AddHostedService<ScopedWorker>();
        new InstaCrawlerApp.ContainerModule().Register(services, config);
        new InstaPersistence.ContainerModule().Register(services, config);
        new SeleniumUtils.ContainerModule().Register(services, config);
    })
    .Build();

using var scope = host.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<InstaDbContext>();
db.Database.Migrate();

host.Run();
