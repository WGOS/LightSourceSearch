using Serilog;

namespace LightSourceSearch.Services.Logging
{
    public interface ILoggerFactory
    {
        ILogger GetLogger(string name, string format = null);
    }
}