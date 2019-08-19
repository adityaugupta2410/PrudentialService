using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WeatherDataAPI.Helpers
{
    public abstract class APIHelper : FileHelper
    {
        #region Protected Method : Generic Method to Fetch data from Open Weather REST Service using HttpClient
        protected async Task<HttpResponseMessage> GetDataFromAPI(Uri APIUri, string[] Headers)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();

            foreach (var item in Headers)
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(item));

            var response = await client.GetAsync(APIUri);
            return response;

        }
        #endregion
    }
}