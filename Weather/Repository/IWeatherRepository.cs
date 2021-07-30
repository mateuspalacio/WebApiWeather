using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Model;

namespace Weather.Repository
{
    public interface IWeatherRepository
    {
        Task<List<WeatherImportantData>> GetAll();

        void PostResponse(WeatherResponse wr);

    }
}
