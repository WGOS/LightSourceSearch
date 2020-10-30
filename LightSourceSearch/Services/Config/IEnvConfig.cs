namespace LightSourceSearch.Services.Config
{
    public interface IEnvConfig
    {
        T Get<T>(string env, T defValue);
    }
}