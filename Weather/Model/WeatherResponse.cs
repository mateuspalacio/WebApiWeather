using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Weather.Model
{
    public class WeatherResponse
    {
        public class Coord
        {
            public double lon { get; set; }
            public double lat { get; set; }
        }

        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

        public class MainPart
        {
            [Key]
            public double temp { get; set; }
            public int pressure { get; set; }
            public int humidity { get; set; }
            public double temp_min { get; set; }
            public double temp_max { get; set; }
        }

        public class Wind
        {
            public double speed { get; set; }
            public int deg { get; set; }
        }

        public class Clouds
        {
            public int all { get; set; }
        }

        public class Sys
        {
            public int type { get; set; }
            public int id { get; set; }
            public double message { get; set; }
            public string country { get; set; }
            public int sunrise { get; set; }
            public int sunset { get; set; }
        }

        [NotMapped]
        public Coord coord { get; set; }
        [NotMapped]
        public List<Weather> weather { get; set; }
        public string @base { get; set; }
        public MainPart main { get; set; }
        public int visibility { get; set; }
        [NotMapped]
        public Wind wind { get; set; }
        [NotMapped]
        public Clouds clouds { get; set; }
        [Key]
        public int dt { get; set; }
        [NotMapped]
        public Sys sys { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
        //public DateTime? dateTime { get; set; }

    }
}
