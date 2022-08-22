using Microsoft.AspNetCore.Mvc;

namespace Wepsys.DianaHr.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };        

        /// <summary>
        /// 
        /// </summary>        
        public WeatherForecastController()
        {            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
#pragma warning disable CA5394 // Do not use insecure randomness
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
#pragma warning restore CA5394 // Do not use insecure randomness
        }
    }
}