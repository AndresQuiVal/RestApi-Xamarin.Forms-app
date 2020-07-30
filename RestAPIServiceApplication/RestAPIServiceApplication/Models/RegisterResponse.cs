using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestAPIServiceApplication.Models
{
    public class RegisterResponse
    {
        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("ModelState")]
        public ModelState ModelState { get; set; }
    }
}
