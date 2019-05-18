using System.Runtime.Serialization;
using MyParcelApi.Net.Exceptions;

namespace MyParcelApi.Net.Wrappers
{
    [DataContract]
    public class ApiWrapper
    {
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public DataWrapper Data { get; set; }

        [DataMember(Name = "errors", EmitDefaultValue = false)]
        public MyParcelError[] Errors { get; set; }

        [DataMember(Name = "message", EmitDefaultValue = false)]
        public string Message { get; set; }
    }
}
