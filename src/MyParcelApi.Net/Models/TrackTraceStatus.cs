using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class TrackTraceStatus
    {
        [DataMember(Name = "current", EmitDefaultValue = false, IsRequired = false)]
        public ShipmentStatus Current { get; set; }

        [DataMember(Name = "main", EmitDefaultValue = false, IsRequired = false)]
        public string Main { get; set; }

        [DataMember(Name = "final", EmitDefaultValue = false)]
        public bool Final { get; set; }
    }
}
