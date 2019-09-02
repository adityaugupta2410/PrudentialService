using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WeatherData.BusinessLayer;
using WeatherData.Helpers;



namespace WeatherDataAPI.Controllers
{
    public class WeatherDataController : ApiController
    {
        private IWeatherRepository _repository;

        public WeatherDataController(IWeatherRepository repository)
        {
            _repository = repository;
        }

        #region Public Method : WEBAPI GET Method to Get the Weather Data 
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                string OutputFolder;
                //For Unit Testing
                if (HttpContext.Current == null)
                {
                    OutputFolder = ConfigurationManager.AppSettings["TestOutputFilePath"] + "Output_" + DateTime.Now.ToShortDateString();
                }
                else
                    OutputFolder = (System.Web.HttpContext.Current.Request.MapPath("~\\OutputFiles\\") + "Output_" + DateTime.Now.ToShortDateString());

               // _repository = new WeatherRepository();

                string response = await _repository.GetWeatherDataFromVendorService(OutputFolder);

                if (response.ToLower().Contains("successful"))
                    return Ok("Output Files Generated successfully on " + DateTime.Now.ToShortDateString() + ". Physical Path of Generated files =" + OutputFolder);

                else if(response.ToLower().Contains("verify"))
                    return BadRequest(response);

                else
                    return InternalServerError(new Exception(response));


            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion



        
    }
}
