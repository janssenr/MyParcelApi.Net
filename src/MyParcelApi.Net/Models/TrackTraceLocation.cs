using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class TrackTraceLocation
    {
        [DataMember(Name = "name", EmitDefaultValue = false, IsRequired = false)]
        public string Name { get; set; }

        [DataMember(Name = "cc", EmitDefaultValue = false, IsRequired = false)]
        public string Country { get; set; }

        [DataMember(Name = "city", EmitDefaultValue = false, IsRequired = false)]
        public string City { get; set; }

        [DataMember(Name = "postal_code", EmitDefaultValue = false)]
        public string PostalCode { get; set; }

        [DataMember(Name = "street", EmitDefaultValue = false)]
        public string Street { get; set; }

        [DataMember(Name = "number", EmitDefaultValue = false)]
        public string Number { get; set; }

        [DataMember(Name = "number_suffix", EmitDefaultValue = false)]
        public string NumberSuffix { get; set; }

        [DataMember(Name = "longitude", EmitDefaultValue = false)]
        public float Longitude { get; set; }

        [DataMember(Name = "latitude", EmitDefaultValue = false)]
        public float Latitude { get; set; }
    }
}
