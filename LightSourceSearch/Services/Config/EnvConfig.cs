using System;
using System.ComponentModel;
using LightSourceSearch.Services.Logging;
using Serilog;

namespace LightSourceSearch.Services.Config
{
    public class EnvConfig : IEnvConfig
    {
        private readonly ILogger _logger;

        public EnvConfig(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.GetLogger("EnvConfig");
        }
        
        public T Get<T>(string env, T defValue)
        { 
            var converter = TypeDescriptor.GetConverter(typeof(T));

            var sVal = Environment.GetEnvironmentVariable(env);
            if (sVal == null)
                return defValue;
            
            T val;

            try
            {
                val = (T) converter.ConvertFromString(sVal);
            }
            catch (Exception e)
            {
                val = defValue;
                _logger.Error(e.ToString());
            }

            return val;
        }
    }
}