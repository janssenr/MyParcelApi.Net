using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class Address
    {
        [DataMember(Name = "cc", EmitDefaultValue = false, IsRequired = true)]
        public string Country { get; set; }

        [DataMember(Name = "region", EmitDefaultValue = false, IsRequired = false)]
        public string Region { get; set; }

        [DataMember(Name = "postal_code", EmitDefaultValue = false, IsRequired = false)]
        public string PostalCode { get; set; }

        [DataMember(Name = "city", EmitDefaultValue = false, IsRequired = false)]
        public string City { get; set; }

        [DataMember(Name = "street", EmitDefaultValue = false, IsRequired = false)]
        public string Street { get; set; }

        [DataMember(Name = "street_additional_info", EmitDefaultValue = false, IsRequired = false)]
        public string StreetAdditionalInfo { get; set; }

        [DataMember(Name = "number", EmitDefaultValue = false, IsRequired = false)]
        public string Number { get; set; }

        [DataMember(Name = "number_suffix", EmitDefaultValue = false, IsRequired = false)]
        public string NumberSuffix { get; set; }

        [DataMember(Name = "person", EmitDefaultValue = false, IsRequired = true)]
        public string Person { get; set; }

        [DataMember(Name = "company", EmitDefaultValue = false, IsRequired = false)]
        public string Company { get; set; }

        [DataMember(Name = "email", EmitDefaultValue = false, IsRequired = false)]
        public string Email { get; set; }

        [DataMember(Name = "phone", EmitDefaultValue = false, IsRequired = false)]
        public string Phone { get; set; }
    }
}
