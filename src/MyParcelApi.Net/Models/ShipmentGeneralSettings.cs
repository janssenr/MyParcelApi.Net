using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class ShipmentGeneralSettings
    {
        [DataMember(Name = "save_recipient_address", EmitDefaultValue = false, IsRequired = false)]
        public bool? SaveRecipientAddress { get; set; }

        [DataMember(Name = "tracktrace", EmitDefaultValue = false, IsRequired = false)]
        public ShipmentGeneralSettingsTrackTrace TrackTrace { get; set; }

        [DataMember(Name = "delivery_notification", EmitDefaultValue = false, IsRequired = false)]
        public bool? DeliveryNotification { get; set; }

        [DataMember(Name = "delivery_notification_email", EmitDefaultValue = false, IsRequired = false)]
        public string[] DeliveryNotificationEmail { get; set; }

        [DataMember(Name = "disable_auto_detect_pickup", EmitDefaultValue = false, IsRequired = false)]
        public bool? DisableAutoDetectPickup { get; set; }
    }
}
