using Floo.Core.Shared.HttpProxy.Attributes;
using System.Threading.Tasks;

namespace Floo.App.Shared
{
    [Authorize]
    public interface IWeatherForecastService
    {
        Task<WeatherForecast[]> GetWeatherForecasts();
    }
}