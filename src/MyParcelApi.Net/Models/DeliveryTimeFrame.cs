using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{

    [DataContract]
    public class DeliveryTimeFrame
    {
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        [DataMember(Name = "date_time", EmitDefaultValue = false)]
        public DateTimeStartEnd DateTime { get; set; }
    }
}
