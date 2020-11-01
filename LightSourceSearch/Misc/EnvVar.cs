using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LightSourceSearch.Misc
{
    /// <summary>
    ///     Get or set environment variable
    /// </summary>
    /// <typeparam name="T">Type of environment variable. Should be a basic type</typeparam>
    public class EnvVar<T>
    {
        private static readonly Dictionary<string, EnvVar<T>> Vars = new Dictionary<string, EnvVar<T>>();
        private readonly TypeConverter _converter;

        private readonly T _defValue;

        private EnvVar(string name, T defaultValue)
        {
            Name = name;
            _defValue = defaultValue;
            _converter = TypeDescriptor.GetConverter(typeof(T));
        }

        /// <summary>
        ///     Name of environment variable
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Value of environment variable
        /// </summary>
        public T Value
        {
            get
            {
                var sVal = Environment.GetEnvironmentVariable(Name);
                if (sVal == null)
                    return _defValue;

                T val;

                try
                {
                    val = (T) _converter.ConvertFromString(sVal);
                }
                catch
                {
                    val = _defValue;
                }

                return val;
            }

            set => Environment.SetEnvironmentVariable(Name, value.ToString());
        }

        /// <summary>
        ///     Get variable
        /// </summary>
        /// <param name="name">Name of variable</param>
        /// <param name="defaultValue">Default value of environment</param>
        /// <returns></returns>
        public static EnvVar<T> Get(string name, T defaultValue)
        {
            if (Vars.ContainsKey(name))
                return Vars[name];

            var envVar = new EnvVar<T>(name, defaultValue);
            Vars.Add(name, envVar);

            return envVar;
        }
    }

    /// <summary>
    ///     Pre-defined environment variables
    /// </summary>
    public static class EnvVar
    {
        /// <summary>
        ///     Speaker pin number
        /// </summary>
        public static readonly EnvVar<int> SpeakerPin = EnvVar<int>.Get("LSS_PIN_SPEAKER", 24);

        /// <summary>
        ///     Laser pin number
        /// </summary>
        public static readonly EnvVar<int> LaserPin = EnvVar<int>.Get("LSS_PIN_LASER", 23);

        /// <summary>
        ///     File logging setting
        /// </summary>
        public static readonly EnvVar<bool> LogToFile = EnvVar<bool>.Get("LSS_LOG_FILE", false);
        
        /// <summary>
        ///     Debug mode
        /// </summary>
        public static readonly EnvVar<bool> DebugMode = EnvVar<bool>.Get("LSS_DEBUG", false);
        
        /// <summary>
        ///     Disable sounds if true
        /// </summary>
        public static readonly EnvVar<bool> SilentMode = EnvVar<bool>.Get("LSS_SILENT", false);
    }
}