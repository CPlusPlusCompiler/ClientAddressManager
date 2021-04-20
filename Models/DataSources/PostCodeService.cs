using ClientAddressManager.Utils;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ClientAddressManager.Models.DataSources
{
    public class PostCodeService : IPostCodeService
    {
        // not injecting because there is no configuration anyways
        private readonly HttpClient httpClient;

        public PostCodeService()
        {
            httpClient = new HttpClient();
        }


        public async Task<Result<string>> GetPostCodeAsync(string address)
        {
            var encodedAddress = HttpUtility.UrlEncode(address);

            var baseUrl = ConfigurationManager.AppSettings["ApiUrl"];
            var apiKey = ConfigurationManager.AppSettings["ApiKey"];
            var url = $"{baseUrl}?term={encodedAddress}&key={apiKey}";

            HttpResponseMessage response = null;

            try
            {
                response = await httpClient.GetAsync(url);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return new Result<string>(ResultCode.ERROR, default, Strings.ERROR_SERVER); 
            }

            string jsonStr;
            if (!response.IsSuccessStatusCode)
                return new Result<string>(ResultCode.ERROR, default, response.ReasonPhrase);
            else
                jsonStr = await response.Content.ReadAsStringAsync();

            var codesResponse = JsonConvert.DeserializeObject<PostltResponse>(jsonStr);

            if (!codesResponse.success)
                return new Result<string>(ResultCode.ERROR, default, codesResponse.message);

            if (codesResponse.data.Count == 0)
                return new Result<string>(ResultCode.ERROR, default, Strings.ERROR_ADDRESS_NOT_FOUND);

            return new Result<string>(ResultCode.SUCCESS, codesResponse.data[0].post_code, null); 
        }
    }
}
