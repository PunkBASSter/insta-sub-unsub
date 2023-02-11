using InstaCrawlerApp;
using InstaCrawlerServiceWindows;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddScoped<Worker>(); 
        new InstaCrawlerApp.ContainerModule().Register(services);
        new InstaPersistence.ContainerModule().RegisterForServiceWorker(services);
    })
    .Build();

host.Run();
