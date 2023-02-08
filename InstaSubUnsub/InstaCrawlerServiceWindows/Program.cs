using InstaCrawlerServiceWindows;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        new InstaCrawlerApp.ContainerModule().Register(services);
        new InstaPersistence.ContainerModule().Register(services);
    })
    .Build();

host.Run();
