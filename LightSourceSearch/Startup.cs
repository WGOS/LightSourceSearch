using System;
using LightSourceSearch.Container;
using LightSourceSearch.Services;
using LightSourceSearch.Services.Config;
using LightSourceSearch.Services.LaserService;
using LightSourceSearch.Services.Logging;
using LightSourceSearch.Services.SpeakerService;
using Microsoft.Extensions.DependencyInjection;

namespace LightSourceSearch
{
    internal class Startup : IContainerStartup
    {
        public void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ILoggerFactory, LoggerFactory>();
            serviceCollection.AddSingleton<IEnvConfig, EnvConfig>();
            serviceCollection.AddSingleton<ISpeaker, Speaker>();
            serviceCollection.AddSingleton<ILaser, Laser>();
        }

        public void ConfigureServices(IServiceProvider serviceProvider)
        {
            
        }
    }
}