using System.Runtime.Serialization;
using MyParcelApi.Net.Models;

namespace MyParcelApi.Net.Wrappers
{
    [DataContract]
    public class ApiWrapper
    {
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public DataWrapper Data { get; set; }

        [DataMember(Name = "errors", EmitDefaultValue = false)]
        public Error[] Errors { get; set; }

        [DataMember(Name = "message", EmitDefaultValue = false)]
        public string Message { get; set; }
    }
}
