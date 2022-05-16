using System.Collections.Generic;
using Newtonsoft.Json;

namespace Pay.Api.Core.Entities
{
    public class ErrorDetails
    {
        public ErrorDetails()
        {
            this.messages = new List<string>();
        }
        public int statusCode { get; set; }
        [JsonIgnore]
        public string errorCode { get; set; }
        public List<string> messages { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static ErrorDetails Create() => new ErrorDetails { };

        public static ErrorDetails Create(int statusCode, string errorCode, List<string> messages) => new ErrorDetails
        {
            statusCode = statusCode,
            errorCode = errorCode,
            messages = messages
        };

        public void AddMessage(string message)
        {
            this.messages.Add(message);
        }
    }
}