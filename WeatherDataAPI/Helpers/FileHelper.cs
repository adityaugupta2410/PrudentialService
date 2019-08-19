using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace WeatherDataAPI.Helpers
{
    public abstract class FileHelper : ApiController
    {
        #region Protected Methods : File Read/Write Methods
        protected string GetJsonFromFile(string Path)
        {
            string Json = File.ReadAllText(Path);
            return Json;
        }

        protected async Task WriteOutPutAsync(string filePath, string text)
        {
            byte[] encodedText = Encoding.Unicode.GetBytes(text);

            using (FileStream sourceStream = new FileStream(filePath,
                FileMode.Create, FileAccess.Write, FileShare.None,
                bufferSize: 4096, useAsync: true))
            {
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            };
        }

        #endregion
    }
}