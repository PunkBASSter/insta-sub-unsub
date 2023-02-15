using InstaCrawlerServiceWindows;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddHostedService<ScopedWorker>();
        new InstaCrawlerApp.ContainerModule().Register(services);
        new InstaPersistence.ContainerModule().Register(services);
    })
    .Build();

host.Run();
