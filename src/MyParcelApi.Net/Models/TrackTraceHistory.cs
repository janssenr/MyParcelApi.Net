using System;
using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class TrackTraceHistory
    {
        [DataMember(Name = "code", EmitDefaultValue = false)]
        public string Code { get; set; }

        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        [DataMember(Name = "time", EmitDefaultValue = false)]
        public DateTime? Time { get; set; }
    }
}
