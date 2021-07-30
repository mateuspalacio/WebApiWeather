using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Weather.DbContexts;
using Weather.Model;
using Weather.Repository;

namespace Weather.Service
{
    public class WeatherService : IWeatherService
    {
        private IWeatherRepository _weatherRepository;
        public WeatherService(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }
        public async Task<WeatherImportantData> ReadApi(string City)

        {
            string URL = "http://api.openweathermap.org/data/2.5/weather?q=" + City + "&appid=ef630c8ebc31f9934c4872b204c38325";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            var lista = await _weatherRepository.GetAll();
            if(lista != null)
            {
                foreach (WeatherImportantData w in lista)
                {
                    if(w.name == City)
                    {
                        return w;
                    }
                }
                
            }
            HttpResponseMessage response = client.GetAsync(URL).Result;  
            if (response.IsSuccessStatusCode)
            {
                
                // Parse the response body.
                var dataObjects = await response.Content.ReadAsStringAsync();
                WeatherResponse wr = JsonConvert.DeserializeObject<WeatherResponse>(dataObjects);
                WeatherImportantData returning = new WeatherImportantData
                {
                    atual = wr.main.temp - 273,
                    max = wr.main.temp_max - 273,
                    min = wr.main.temp_min - 273,
                    name = wr.name
                };
                //wr.dateTime = DateTime.Now;
                _weatherRepository.PostResponse(wr);
                return returning;
                

            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return null;
            }

        }
    }
}
