using LightSourceSearch.Container;

namespace LightSourceSearch
{
    internal class Program
    {
        internal static string[] Arguments { get; private set; }

        private static void Main(string[] args)
        {
            Arguments = args;

            var appContainer = new AppContainer<Startup, EntryPoint>();
            appContainer.Initialize();
            appContainer.Run();
        }
    }
}