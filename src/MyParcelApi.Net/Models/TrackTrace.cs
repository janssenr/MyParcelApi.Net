using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class TrackTrace
    {
        [DataMember(Name = "shipment_id", EmitDefaultValue = false)]
        public int ShipmentId { get; set; }

        [DataMember(Name = "code", EmitDefaultValue = false)]
        public string Code { get; set; }

        [DataMember(Name = "final", EmitDefaultValue = false)]
        public bool Final { get; set; }

        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        [DataMember(Name = "time", EmitDefaultValue = false)]
        public string Time { get; set; }

        [DataMember(Name = "history", EmitDefaultValue = false)]
        public TrackTraceHistory[] History { get; set; }
    }
}
