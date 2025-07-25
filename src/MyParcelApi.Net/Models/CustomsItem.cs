using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class CustomsItem
    {
        [DataMember(Name = "description", EmitDefaultValue = false, IsRequired = true)]
        public string Description { get; set; }

        [DataMember(Name = "amount", EmitDefaultValue = false, IsRequired = true)]
        public int Amount { get; set; }

        [DataMember(Name = "weight", EmitDefaultValue = true, IsRequired = true)]
        public int Weight { get; set; }

        [DataMember(Name = "item_value", EmitDefaultValue = false, IsRequired = true)]
        public Price ItemValue { get; set; }

        [DataMember(Name = "classification", EmitDefaultValue = false, IsRequired = true)]
        public string Classification { get; set; }

        [DataMember(Name = "country", EmitDefaultValue = false, IsRequired = true)]
        public string Country { get; set; }
    }
}
