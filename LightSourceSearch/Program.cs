using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using LightSourceSearch.Container;
using Unosquare.RaspberryIO.Abstractions;

namespace LightSourceSearch
{
    internal class Program
    {
        static internal string[] Arguments { get; private set; }
        
        private static void Main(string[] args)
        {
            Arguments = args;
            
            var appContainer = new AppContainer<Startup, EntryPoint>();
            appContainer.Initialize();
            appContainer.Run();
        }
    }
}