using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherData.BusinessLayer
{
   public interface IWeatherRepository
    {
        Task<string> GetWeatherDataFromVendorService(string OutputFolder);

    }
}
