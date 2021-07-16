using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class Pickup
    {
        [DataMember(Name = "address", EmitDefaultValue = false)]
        public Address Address { get; set; }

        [DataMember(Name = "possibilities", EmitDefaultValue = false)]
        public PickupOptionsPossibility[] Possibilities { get; set; }

        [DataMember(Name = "location", EmitDefaultValue = false)]
        public PickupOptions<Moment[]> Location { get; set; }
    }
}
