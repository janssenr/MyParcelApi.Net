using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class Delivery
    {
        [DataMember(Name = "date", EmitDefaultValue = false)]
        public DateTimeStartEnd Date { get; set; }

        [DataMember(Name = "possibilities", EmitDefaultValue = false)]
        public DeliveryPossibility[] Possibilities { get; set; }
    }
}
