using System;
using Newtonsoft.Json;

namespace mhlabs.feature_toggle.client.Services.Responses
{
    public class FeatureToggleResponse : IFeatureToggleResponse
    {
        [JsonProperty("active")]    
        public bool Active { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }
        
        [JsonProperty("timestamp")]
        public long TimeStamp => DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}
