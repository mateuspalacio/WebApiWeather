using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Model;

namespace Weather.Service
{
    public interface IWeatherService
    {
        public Task<WeatherImportantData> ReadApi(string City);
    }
}
