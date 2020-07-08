using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class Price
    {
        [DataMember(Name = "amount", EmitDefaultValue = true, IsRequired = true)]
        public int? Amount { get; set; }

        [DataMember(Name = "currency", EmitDefaultValue = false, IsRequired = true)]
        public string Currency { get; set; }
    }
}
