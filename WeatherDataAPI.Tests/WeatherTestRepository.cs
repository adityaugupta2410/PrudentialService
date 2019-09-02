using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherData.BusinessLayer;
using WeatherData.Entities;

namespace WeatherDataAPI.Tests
{
   public class WeatherTestRepository :IWeatherRepository
    {
        public async Task<string> GetWeatherDataFromVendorService(string OutputFolder)
        {
            //var testData = new CityWeatherEntity()
            //{
            //    coord = new Coord()
            //    {
            //        lon = -0.09,
            //        lat = 51.51
            //    },
            //    clouds = new Clouds()
            //    {
            //        all = 88
            //    },
            //    dt = 1566122970,
            //    main = new Main()
            //    {
            //        temp = 14.56,
            //        pressure = 1005,
            //        humidity = 87,
            //        temp_min = 14.0,
            //        temp_max = 15.0
            //    },
            //    id = 2643741,
            //    name = "City of London",
            //    sys = new Sys()
            //    {
            //        country = "GB",
            //        timezone = 3600,
            //        sunrise = 1566103808,
            //        sunset = 1566155927
            //    },
            //    visibility = 6000,
            //    weather = new System.Collections.Generic.List<Weather>()
            //    {
            //        new Weather()
            //        {
            //        id = 521,
            //        main = "Rain",
            //        description = "shower rain",
            //        icon = "09d"
            //        }

            //    },
            //    wind = new Wind()
            //    {
            //        speed = 6.7,
            //        deg = 260
            //    }
            //};
            string ActualResult = "Operation Successful";
            return ActualResult;
        }
    }
}
