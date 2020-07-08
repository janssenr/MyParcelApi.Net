using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class LocationTime
    {
        [DataMember(Name = "start", EmitDefaultValue = false)]
        public string Start { get; set; }

        [DataMember(Name = "type", EmitDefaultValue = false)]
        public int Type { get; set; }

        [DataMember(Name = "price", EmitDefaultValue = false)]
        public Price Price { get; set; }
    }
}
