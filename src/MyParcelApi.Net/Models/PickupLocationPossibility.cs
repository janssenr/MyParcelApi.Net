using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class PickupLocationPossibility
    {
        [DataMember(Name = "delivery_type_id", EmitDefaultValue = false, IsRequired = false)]
        public int DeliveryTypeId { get; set; }

        [DataMember(Name = "delivery_type", EmitDefaultValue = false, IsRequired = false)]
        public DeliveryType DeliveryType { get; set; }

        [DataMember(Name = "moment", EmitDefaultValue = false, IsRequired = false)]
        public Moment Moment { get; set; }
    }
}
