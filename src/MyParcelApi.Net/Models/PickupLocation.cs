using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class PickupLocation
    {
        [DataMember(Name = "city", EmitDefaultValue = false, IsRequired = false)]
        public string City { get; set; }

        [DataMember(Name = "location_name", EmitDefaultValue = false)]
        public string LocationName { get; set; }

        [DataMember(Name = "number", EmitDefaultValue = false)]
        public string Number { get; set; }

        [DataMember(Name = "postal_code", EmitDefaultValue = false)]
        public string PostalCode { get; set; }

        [DataMember(Name = "street", EmitDefaultValue = false)]
        public string Street { get; set; }

        [DataMember(Name = "location_code", EmitDefaultValue = false)]
        public string LocationCode { get; set; }

        [DataMember(Name = "retail_network_id", EmitDefaultValue = false)]
        public string RetailNetworkId { get; set; }
    }
}
