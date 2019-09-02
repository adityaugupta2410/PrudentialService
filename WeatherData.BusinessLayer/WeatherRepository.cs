using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using WeatherData.Entities;
using WeatherData.Helpers;

namespace WeatherData.BusinessLayer
{
    public class WeatherRepository : IWeatherRepository
    {
        public async Task<string> GetWeatherDataFromVendorService( string OutputFolder)
        {
            CityData cityData;
            string CityIds = GetCityIdsFromFile();

            ValidationHelper validationHelper = new ValidationHelper();
            if (string.IsNullOrEmpty(CityIds) || !validationHelper.ValidateCityIds(CityIds))
            {
                return "The Input file doesn't contain valid cityId's. Please verify the input file and re-try!";
            }

            using (APIHelper helper = new APIHelper())
            {
                var response = await helper.GetDataFromAPI(new Uri(string.Format(ConfigurationManager.AppSettings["WeatherByCityId"], CityIds.ToString(), ConfigurationManager.AppSettings["APISecurityKey"])), new string[] { "application/json" });
                if (response != null && response.IsSuccessStatusCode)
                {

                    cityData = JsonConvert.DeserializeObject<CityData>(
                                 await response.Content.ReadAsStringAsync());
                    if (cityData != null)
                    {
                       
                        FileHelper fileHelper = new FileHelper();
                                             
                        if (!Directory.Exists(OutputFolder))
                        {
                            Directory.CreateDirectory(OutputFolder);
                        }
                        foreach (var item in cityData.list)
                        {
                            await fileHelper.WriteOutPutAsync(OutputFolder + "\\" + item.name + ".json", JsonConvert.SerializeObject(item, new JsonSerializerSettings() { Formatting = Formatting.Indented }));
                        }

                        return "Operation Successful";
                    }
                    else
                        return "Error Occured While Deserializing.";
                }
                else
                    return "Null/Blank response received from API.";
            }
        }

        #region Private Method : Read  Input File and Get CityIds
        private string GetCityIdsFromFile()
        {
            string CityIds = string.Empty, Path = string.Empty;
            FileHelper helper = new FileHelper();
            //For Unit Testing
            if (HttpContext.Current == null)
            {
                Path = ConfigurationManager.AppSettings["TestInputFilePath"] + ConfigurationManager.AppSettings["InputFileName"];
            }
            else
                Path = HttpContext.Current.Request.MapPath("~\\InputFiles\\") + ConfigurationManager.AppSettings["InputFileName"];

            string Json = helper.GetJsonFromFile(Path);
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
