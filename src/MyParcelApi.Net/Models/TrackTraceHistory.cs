using System;
using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class TrackTraceHistory
    {
        [DataMember(Name = "code", EmitDefaultValue = false)]
        public string Code { get; set; }

        [DataMember(Name = "status", EmitDefaultValue = false, IsRequired = false)]
        public ShipmentStatus Status { get; set; }

        [DataMember(Name = "main", EmitDefaultValue = false, IsRequired = false)]
        public string Main { get; set; }

        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        [DataMember(Name = "time", EmitDefaultValue = false)]
        public DateTime? Time { get; set; }
    }
}
