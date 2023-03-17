using InstaCrawlerApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaCrawlerServiceWindows.Jobs
{
    public class QuartzJobWrapper<T> where T: JobBase //Todo implement IJob
    {
        private readonly IServiceProvider _serviceProvider;

        public QuartzJobWrapper(IServiceProvider serviceProvider) 
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Execute(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            
            var job = scope.ServiceProvider.GetRequiredService<T>();
            await job.Execute(stoppingToken);
        }
    }
}
