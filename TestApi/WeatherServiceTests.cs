using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
    public class WeatherServiceTests
    {
        public WeatherService _weatherService;
        public WeatherRepository _weatherRepository;

        public static IConfiguration Configuration { get; }
        public WeatherServiceTests()
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
            _weatherRepository = new WeatherRepository(configuration.GetConnectionString("MySqlDbConnection"));
            _weatherService = new WeatherService(_weatherRepository);
        }
        [Fact]
        public async Task It_Should_Read_Api()
        {
            var result = await _weatherService.ReadApi("Tokyo");

            Assert.IsType<WeatherImportantData>(result);

        }
    }
}
