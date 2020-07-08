using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class DeliveryPossibility
    {
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        [DataMember(Name = "delivery_time_frames", EmitDefaultValue = false)]
        public DeliveryTimeFrame[] DeliveryTimeFrames { get; set; }

        [DataMember(Name = "shipment_options", EmitDefaultValue = false)]
        public DeliveryShipmentOption[] ShipmentOptions { get; set; }
    }
}
