using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.DbContexts;
using Weather.Model;

namespace Weather.Repository
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly string _connectionString;
        public WeatherRepository(string connectionString)
        {
            _connectionString = connectionString;   
        }

        public async Task<List<WeatherImportantData>> GetAll()
        {
            List<WeatherImportantData> list = new List<WeatherImportantData>();
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using(var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM weatherresponses inner join mainpart on weatherresponses.maintemp = mainpart.temp WHERE from_unixtime(dt) >= NOW() - INTERVAL 20 MINUTE";
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            int maxindex = reader.GetOrdinal("temp_max");
                            int minindex = reader.GetOrdinal("temp_min");
                            int atualindex = reader.GetOrdinal("temp");
                            int nameindex = reader.GetOrdinal("name");

                            WeatherImportantData wid = new WeatherImportantData
                            {
                                max = reader.GetDouble(maxindex),
                                min = reader.GetDouble(minindex),
                                atual = reader.GetDouble(atualindex),
                                name = reader.GetString(nameindex)
                            };
                            list.Add(wid);
                        }

                    }
                }

            }
            return list;
        }

        public void PostResponse(WeatherResponse wr)
        {
            var context = new ApplicationDbContext();
            context.WeatherResponses.Add(wr);
            context.SaveChanges();
        }
    }
}
