using Floo.App.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Floo.App.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase, IWeatherForecastService
    {
        private IWeatherForecastService service;

        public WeatherForecastController(IWeatherForecastService service)
        {
            this.service = service;
        }

        [HttpGet("")]
        public Task<WeatherForecast[]> GetWeatherForecasts()
        {
            return service.GetWeatherForecasts();
        }
    }
}