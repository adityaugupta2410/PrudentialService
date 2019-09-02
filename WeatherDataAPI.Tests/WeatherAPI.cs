using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Web.Http.Results;
using WeatherData.Entities;
using WeatherDataAPI.Controllers;

namespace WeatherDataAPI.Tests
{
    [TestClass]
    public class WeatherAPI
    {
        [TestMethod]
        public async Task GetWeatherData_ShouldReturnWeatherData()
        {
            var controller = new WeatherDataController(new WeatherTestRepository());
            // Act on Test  
            var actionResult = await controller.Get();
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<string>));
        }

       
    }
}
