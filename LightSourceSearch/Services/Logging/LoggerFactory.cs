using LightSourceSearch.Misc;
using Serilog;

namespace LightSourceSearch.Services.Logging
{
    public class LoggerFactory : ILoggerFactory
    {
        public ILogger GetLogger(string name, string format = null)
        {
            var logger = new LoggerConfiguration();
            format ??= $"[{{Timestamp:HH:mm:ss}}] [{{Level:u3}}] [{name}] {{Message:lj}}{{NewLine}}";

#if DEBUG
            logger = logger.MinimumLevel.Verbose();
#else
            logger = logger.MinimumLevel.Information();
#endif

            var factory = logger
                .WriteTo
                .Console(outputTemplate: format);

            if (EnvVar.LogToFile.Value)
                factory.WriteTo.File("log.txt", outputTemplate: format);

            return factory.CreateLogger();
        }
    }
}