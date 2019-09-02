using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WeatherData.Helpers
{
    public class APIHelper :IDisposable
    {
        HttpClient client = new HttpClient();

        public void Dispose()
        {
            if (client != null)
            {
                client.Dispose();
                GC.SuppressFinalize(this);

            }
        }

        #region Public Method : Generic Method to Fetch data from Open Weather REST Service using HttpClient
        public async Task<HttpResponseMessage> GetDataFromAPI(Uri APIUri, string[] Headers)
        {
           
            client.DefaultRequestHeaders.Accept.Clear();

            foreach (var item in Headers)
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(item));

            var response = await client.GetAsync(APIUri);
            return response;

        }
        #endregion
    }
}