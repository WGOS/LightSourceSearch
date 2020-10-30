namespace LightSourceSearch.Services.LaserService
{
    public interface ILaser
    {
        bool Turned { get; set; }
        void Initialize();
    }
}