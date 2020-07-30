using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestAPIServiceApplication.Models
{
    public class ModelState
    {
        [JsonProperty("model.ConfirmPassword")]
        public string[] ModelConfirmPassword { get; set; }

        [JsonProperty("model.Password")]
        public string[] ModelPassword { get; set; }

        [JsonProperty("model.Email")]
        public string[] ModelEmail { get; set; }

        [JsonProperty("model.FirstName")]
        public string[] ModelFirstName { get; set; }

        [JsonProperty("")]
        public string[] UnexError { get; set; } // Unexpected errors
    }
}
