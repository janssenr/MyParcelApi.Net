using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class Error
    {
        [DataMember(Name = "code", EmitDefaultValue = false)]
        public int Code { get; set; }

        [DataMember(Name = "message", EmitDefaultValue = false)]
        public string Message { get; set; }

        //[DataMember(Name = "human", EmitDefaultValue = false)]
        //public string Human { get; set; }

        [DataMember(Name = "fields", EmitDefaultValue = false)]
        public string[] Fields { get; set; }

        [DataMember(Name = "human", EmitDefaultValue = false)]
        public string[] Human { get; set; }
    }
}
