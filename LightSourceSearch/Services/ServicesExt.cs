using LightSourceSearch.Services.Config;
using LightSourceSearch.Services.LaserService;
using LightSourceSearch.Services.Logging;
using LightSourceSearch.Services.Speaker;
using Microsoft.Extensions.DependencyInjection;

namespace LightSourceSearch.Services
{
    public static class ServicesExt
    {
        public static void AddLssServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ILoggerFactory, LoggerFactory>();
            serviceCollection.AddSingleton<IEnvConfig, EnvConfig>();
            serviceCollection.AddSingleton<ISpeaker, Speaker.Speaker>();
            serviceCollection.AddSingleton<ILaser, Laser>();
        }
    }
}