using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class ShipmentLabelDownloadLink
    {
        [DataMember(Name = "url", EmitDefaultValue = false)]
        public string Url { get; set; }
    }
}
