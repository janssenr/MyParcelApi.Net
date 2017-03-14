using System.Runtime.Serialization;

namespace MyParcelApi.Net.Wrappers
{
    [DataContract]
    public class ApiWrapper
    {
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public DataWrapper Data { get; set; }
    }
}
