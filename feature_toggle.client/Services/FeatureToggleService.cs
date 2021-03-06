using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using mhlabs.feature_toggle.client.Services.Responses;
using MhLabs.AwsSignedHttpClient;
using Newtonsoft.Json;

namespace mhlabs.feature_toggle.client.Services
{
    public class FeatureToggleService : IFeatureToggleService
    {
        private readonly HttpClient _httpClient;
        private readonly IFeatureToggleConfiguration _configuration;

        public FeatureToggleService(HttpClient httpClient, IFeatureToggleConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<IFeatureToggleResponse> Get(string flagName, string userKey, bool defaultValue = default(bool), CancellationToken cancellationToken = default) 
        {
            var url = string.Format(_configuration.ApiPathFormat, flagName, userKey);
            var response = await _httpClient.SendAsync<FeatureToggleServiceContract>(HttpMethod.Get, url, cancellationToken: cancellationToken);

            return new FeatureToggleResponse()
            {
                Enabled = response.Active,
                Error = null
            };
        }

        public class FeatureToggleServiceContract
        {
            [JsonProperty("active")]
            public bool Active { get; set; }
        }
    }
}
