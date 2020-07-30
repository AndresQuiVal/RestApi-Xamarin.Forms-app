using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RestAPIServiceApplication.Models
{
    public class TokenResponse
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }
        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }
        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }
        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }
        [JsonProperty(PropertyName = ".issued")]
        public string Issued { get; set; }
        [JsonProperty(PropertyName = ".expires")]
        public string Expires { get; set; }
        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; } //error
        [JsonProperty(PropertyName = "error_description")]
        public string ErrorDescription { get; set; } //error_description
    }
}
