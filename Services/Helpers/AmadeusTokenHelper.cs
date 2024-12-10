using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Services.Helpers
{
    public class AmadeusTokenHelper
    {
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _config;

        public AmadeusTokenHelper(IMemoryCache cache, IConfiguration config)
        {
            _cache = cache;
            _config = config;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            if (_cache.TryGetValue("AmadeusToken", out string token))
            {
                Console.WriteLine("Token found in cache.");
                return token;
            }

            Console.WriteLine("Requesting new token...");

            var client = new RestClient("https://test.api.amadeus.com");
            var request = new RestRequest("/v1/security/oauth2/token", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", "MA8HBGd6lAsb20NVvP7XTilgG6X1bHXp");
            request.AddParameter("client_secret", "4OgyYAzkpZf5cmH5");

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
                throw new Exception("Failed to retrieve access token.");

            token = JObject.Parse(response.Content)["access_token"]?.ToString();
            int expiresIn = JObject.Parse(response.Content)["expires_in"]?.ToObject<int>() ?? 1800;

            _cache.Set("AmadeusToken", token, TimeSpan.FromSeconds(expiresIn - 60));

            return token;
        }    
    }
}
