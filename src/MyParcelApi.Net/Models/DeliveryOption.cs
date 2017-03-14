using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class DeliveryOption
    {
        [DataMember(Name = "date", EmitDefaultValue = false)]
        public string Date { get; set; }

        [DataMember(Name = "time", EmitDefaultValue = false)]
        public DeliveryOptionTime[] Times { get; set; }
    }
}
