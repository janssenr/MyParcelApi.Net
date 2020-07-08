using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class ShipmentLabelCreated
    {
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public WebhookStatus Status { get; set; }

        [DataMember(Name = "shipment_ids", EmitDefaultValue = false)]
        public int[] ShipmentIds { get; set; }

        [DataMember(Name = "printer_identifier", EmitDefaultValue = false)]
        public string PrinterIdentifier { get; set; }

        [DataMember(Name = "pdf", EmitDefaultValue = false)]
        public string Pdf { get; set; }
    }
}
