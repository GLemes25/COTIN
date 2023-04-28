using Newtonsoft.Json;
using RestSharp;

namespace Common.Extensions.Logic
{
    public class TokenClientExtension
    {
        public static async Task<string> GetToken(TokenAppSettings settings)
        {
            var client = new RestClient(settings.Url);
            var request = new RestRequest(string.Empty, Method.Post);

            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded",
                                 string.Format("grant_type={0}&client_id={1}&client_secret={2}", settings.Grant_type, settings.Client_id, settings.Client_secret),
                                 ParameterType.RequestBody);

            var response = await client.ExecuteAsync<string>(request);

            var jsonResponse = JsonConvert.DeserializeObject<keycloakResponse>(response.Content);
            return jsonResponse?.access_token;

        }
    }
}
