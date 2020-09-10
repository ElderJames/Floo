using System.Threading.Tasks;

namespace Floo.App.Shared
{
    public interface IWeatherForecastService
    {
        Task<WeatherForecast[]> GetWeatherForecasts();
    }
}