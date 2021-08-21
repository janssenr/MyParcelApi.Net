using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class PickupLocation
    {
        [DataMember(Name = "address", EmitDefaultValue = false)]
        public Address Address { get; set; }

        [DataMember(Name = "possibilities", EmitDefaultValue = false)]
        public PickupLocationPossibility[] Possibilities { get; set; }

        [DataMember(Name = "location", EmitDefaultValue = false)]
        public PickupOption<Moment[]> Location { get; set; }
    }
}
