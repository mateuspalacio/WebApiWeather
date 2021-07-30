using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Controllers;
using Weather.Model;
using Weather.Repository;
using Weather.Service;
using Xunit;

namespace TestApi
{
    public class WeatherControllerTests
    {
        public WeatherForecastController _weatherForecastController;
        public WeatherService _weatherService;
        public WeatherRepository _weatherRepository;
        public IConfiguration Configuration { get; }
        public WeatherControllerTests()
        {
            _weatherRepository = new WeatherRepository("server=localhost;database=weatherMateus;user=root;password=rootpassword123");
            _weatherService = new WeatherService(_weatherRepository);
            _weatherForecastController = new WeatherForecastController(_weatherService);
        }
        [Fact]
        public async Task It_Should_Get_Weather_Temp()
        {
            var result = await _weatherForecastController.Get("Fortaleza");

            Assert.IsType<WeatherImportantData>(result);
        }
    }
}
