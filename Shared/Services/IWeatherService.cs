namespace Shared.Services
{
    public interface IWeatherService
    {
        Task<string> GetWeatherAsync();
    }
}
