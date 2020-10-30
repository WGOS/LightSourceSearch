using System;
using Microsoft.Extensions.DependencyInjection;

namespace LightSourceSearch.Container
{
    public interface IContainerStartup
    {
        void RegisterServices(IServiceCollection serviceCollection);
        void ConfigureServices(IServiceProvider serviceProvider);
    }
}