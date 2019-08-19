using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WeatherDataAPI.Helpers;
using WeatherDataAPI.Models;

namespace WeatherDataAPI.Controllers
{
    public class WeatherDataController : APIHelper
    {
        #region Public Method : WEBAPI GET Method to Get the Weather Data 
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                string CityIds = GetCityIdsFromFile();
                if (string.IsNullOrEmpty(CityIds) || !ValidateCityIds(CityIds))
                {
                    return BadRequest("The Input file doesn't contain valid cityId's. Please verify the input file and re-try!");
                }

                CityData cityData;
                var response = await GetDataFromAPI(new Uri(string.Format(ConfigurationManager.AppSettings["WeatherByCityId"], CityIds.ToString(), ConfigurationManager.AppSettings["APISecurityKey"])), new string[] { "application/json" });
                if (response != null && response.IsSuccessStatusCode)
                {

                    cityData = JsonConvert.DeserializeObject<CityData>(
                                 await response.Content.ReadAsStringAsync());
                    if (cityData != null)
                    {
                        string OutputFolder;

                        //For Unit Testing
                        if (HttpContext.Current == null)
                        {
                            OutputFolder = ConfigurationManager.AppSettings["TestOutputFilePath"] + "Output_" + DateTime.Now.ToShortDateString();
                        }
                        else
                            OutputFolder = (System.Web.HttpContext.Current.Request.MapPath("~\\OutputFiles\\") + "Output_" + DateTime.Now.ToShortDateString());
                        if (!Directory.Exists(OutputFolder))
                        {
                            Directory.CreateDirectory(OutputFolder);
                        }
                        foreach (var item in cityData.list)
                        {
                            await WriteOutPutAsync(OutputFolder + "\\" + item.name + ".json", JsonConvert.SerializeObject(item, new JsonSerializerSettings() { Formatting = Formatting.Indented }));
                        }
                        return Ok("Output Files Generated successfully on " + DateTime.Now.ToShortDateString() + ". Physical Path of Generated files =" + OutputFolder);
                    }
                    else
                        return InternalServerError(new Exception("Error Occured While Deserializing."));
                }
                else
                    return InternalServerError(new Exception("Null/Blank response received from API."));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion

        #region Private Method : Custom Validation to check if Ids are all Int64 Type
        private bool ValidateCityIds(string CityIds)
        {
            bool IsValidInput = true;
            long temp = 0;
            if (!string.IsNullOrEmpty(CityIds))
            {
                if (CityIds.Contains(","))
                {
                    string[] cityId = CityIds.Split(',');

                    foreach (var id in cityId)
                    {
                        IsValidInput = IsValidInput && Int64.TryParse(id, out temp);
                    }
                }
                else
                    IsValidInput = IsValidInput && Int64.TryParse(CityIds, out temp);
            }
            else
                IsValidInput = false;

            return IsValidInput;
        }

        #endregion

        #region Private Method : Read  Input File and Get CityIds
        private string GetCityIdsFromFile()
        {
            string CityIds = string.Empty, Path = string.Empty;
            //For Unit Testing
            if (HttpContext.Current == null)
            {
                Path = ConfigurationManager.AppSettings["TestInputFilePath"] + ConfigurationManager.AppSettings["InputFileName"];
            }
            else
                Path = HttpContext.Current.Request.MapPath("~\\InputFiles\\") + ConfigurationManager.AppSettings["InputFileName"];

            string Json = GetJsonFromFile(Path);
            List<InputEntity> inputEntities = JsonConvert.DeserializeObject<List<InputEntity>>(Json);
            foreach (InputEntity item in inputEntities)
            {
                if (string.IsNullOrEmpty(CityIds))
                    CityIds += item.CityId;
                else
                    CityIds += "," + item.CityId;
            }

            return CityIds;
        }
        #endregion
    }
}
