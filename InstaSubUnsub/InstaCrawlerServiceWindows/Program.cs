using InstaCrawlerApp;
using InstaCrawlerServiceWindows;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddHostedService<Worker>(); 
        new InstaCrawlerApp.ContainerModule().RegisterForServiceWorker(services);
        new InstaPersistence.ContainerModule().RegisterForServiceWorker(services);
    })
    .Build();

host.Run();
