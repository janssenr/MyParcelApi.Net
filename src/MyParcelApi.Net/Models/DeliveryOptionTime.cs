using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class DeliveryOptionTime
    {
        [DataMember(Name = "start", EmitDefaultValue = false)]
        public string Start { get; set; }

        [DataMember(Name = "end", EmitDefaultValue = false)]
        public string End { get; set; }

        [DataMember(Name = "price", EmitDefaultValue = false)]
        public Price Price { get; set; }

        [DataMember(Name = "price_comment", EmitDefaultValue = false)]
        public string PriceComment { get; set; }

        [DataMember(Name = "comment", EmitDefaultValue = false)]
        public string Comment { get; set; }

        [DataMember(Name = "type", EmitDefaultValue = false)]
        public int Type { get; set; }
    }
}
