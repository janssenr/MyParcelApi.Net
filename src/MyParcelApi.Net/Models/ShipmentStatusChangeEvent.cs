using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class ShipmentStatusChangeEvent
    {
        [DataMember(Name = "shipment_id", EmitDefaultValue = false)]
        public int ShipmentId { get; set; }

        [DataMember(Name = "account_id", EmitDefaultValue = false)]
        public int AccountId { get; set; }

        [DataMember(Name = "shop_id", EmitDefaultValue = false)]
        public int ShopId { get; set; }

        [DataMember(Name = "status", EmitDefaultValue = false)]
        public ShipmentStatus Status { get; set; }

        [DataMember(Name = "barcode", EmitDefaultValue = false)]
        public string Barcode { get; set; }
    }
}
