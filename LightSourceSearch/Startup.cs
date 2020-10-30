using System;
using LightSourceSearch.Container;
using LightSourceSearch.Services;
using LightSourceSearch.Services.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace LightSourceSearch
{
    internal class Startup : IContainerStartup
    {
        public void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddLssServices();
        }

        public void ConfigureServices(IServiceProvider serviceProvider)
        {
            
        }
    }
}