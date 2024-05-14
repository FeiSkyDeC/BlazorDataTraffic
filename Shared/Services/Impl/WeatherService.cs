namespace Shared.Services.Impl
{
    public class WeatherService:IWeatherService
    {
        public async Task<string> GetWeatherAsync()
        {
            await Task.Delay(3000);
            return "Sunny";
        }
    }
}
