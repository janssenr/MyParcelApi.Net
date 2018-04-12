using System;
using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class ShipmentGeneralSettingsTrackTrace
    {
        [DataMember(Name = "bcc", EmitDefaultValue = false, IsRequired = false)]
        public bool Bcc { get; set; }

        [DataMember(Name = "bcc_email", EmitDefaultValue = false, IsRequired = false)]
        public string BccEmail { get; set; }

        [DataMember(Name = "from_address_email", EmitDefaultValue = false, IsRequired = false)]
        public string FromAddressEmail { get; set; }

        [DataMember(Name = "from_address_company", EmitDefaultValue = false, IsRequired = false)]
        public string FromAddressCompany { get; set; }

        [DataMember(Name = "email_on_handed_to_courier", EmitDefaultValue = false, IsRequired = false)]
        public bool EmailOnHandedToCourier { get; set; }

        [DataMember(Name = "send_track_trace_emails", EmitDefaultValue = false, IsRequired = false)]
        public bool SendTrackTraceEmails { get; set; }

        [DataMember(Name = "delivery_notification", EmitDefaultValue = false, IsRequired = false)]
        public bool DeliveryNotification { get; set; }

        [DataMember(Name = "carrier_email_basic_notification", EmitDefaultValue = false, IsRequired = false)]
        public bool CarrierEmailBasicNotification { get; set; }

        [DataMember(Name = "send_to", EmitDefaultValue = false, IsRequired = false)]
        public string SendTo { get; set; }

        [DataMember(Name = "send_on", EmitDefaultValue = false, IsRequired = false)]
        //public DateTime SendOn { get; set; }
        public string SendOn { get; set; }
    }
}
