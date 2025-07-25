using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class ReturnShipment
    {
        [DataMember(Name = "shop_id", EmitDefaultValue = false, IsRequired = false)]
        public int ShopId { get; set; }

        [DataMember(Name = "parent", EmitDefaultValue = false, IsRequired = false)]
        public int? Parent { get; set; }

        [DataMember(Name = "carrier", EmitDefaultValue = false, IsRequired = true)]
        public Carrier Carrier { get; set; }

        [DataMember(Name = "email", EmitDefaultValue = false, IsRequired = false)]
        public string Email { get; set; }

        [DataMember(Name = "name", EmitDefaultValue = false, IsRequired = true)]
        public string Name { get; set; }

        [DataMember(Name = "sender", EmitDefaultValue = false, IsRequired = false)]
        public Address Sender { get; set; }

        [DataMember(Name = "options", EmitDefaultValue = false, IsRequired = false)]
        public ShipmentOptions Options { get; set; }

        [DataMember(Name = "general_settings", EmitDefaultValue = false, IsRequired = false)]
        public ShipmentGeneralSettings GeneralSettings { get; set; }
    }
}
