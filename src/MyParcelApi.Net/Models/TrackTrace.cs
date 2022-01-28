using System;
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

        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        [DataMember(Name = "time", EmitDefaultValue = false)]
        public DateTime? Time { get; set; }

        [DataMember(Name = "link_consumer_portal", EmitDefaultValue = false)]
        public string LinkConsumerPortal { get; set; }

        [DataMember(Name = "link_tracktrace", EmitDefaultValue = false)]
        public string LinkTrackTrace { get; set; }

        [DataMember(Name = "recipient", EmitDefaultValue = false)]
        public Address Recipient { get; set; }

        [DataMember(Name = "sender", EmitDefaultValue = false)]
        public Address Sender { get; set; }

        [DataMember(Name = "options", EmitDefaultValue = false)]
        public ShipmentOptions Options { get; set; }

        [DataMember(Name = "pickup", EmitDefaultValue = false)]
        public PickupOption<string[]> Pickup { get; set; }

        [DataMember(Name = "delayed", EmitDefaultValue = false)]
        public bool Delayed { get; set; }

        [DataMember(Name = "location", EmitDefaultValue = false)]
        public TrackTraceLocation Location { get; set; }

        [DataMember(Name = "status", EmitDefaultValue = false)]
        public TrackTraceStatus Status { get; set; }

        [DataMember(Name = "history", EmitDefaultValue = false)]
        public TrackTraceHistory[] History { get; set; }

        [DataMember(Name = "delivery_moment_type", EmitDefaultValue = false)]
        public string DeliveryMomentType { get; set; }

        [DataMember(Name = "delivery_moment", EmitDefaultValue = false)]
        public Moment DeliveryMoment { get; set; }
    }
}
