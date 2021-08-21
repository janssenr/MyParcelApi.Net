using System.Runtime.Serialization;
using MyParcelApi.Net.Models;

namespace MyParcelApi.Net.Wrappers
{
    [DataContract]
    public class DataWrapper
    {
        [DataMember(Name = "shipments", EmitDefaultValue = false)]
        public Shipment[] Shipments { get; set; }

        [DataMember(Name = "return_shipments", EmitDefaultValue = false)]
        public ReturnShipment[] ReturnShipments { get; set; }

        [DataMember(Name = "ids", EmitDefaultValue = false)]
        public ObjectId[] Ids { get; set; }

        [DataMember(Name = "tracktraces", EmitDefaultValue = false)]
        public TrackTrace[] TrackTraces { get; set; }

        [DataMember(Name = "payment_instructions", EmitDefaultValue = false)]
        public PaymentInstruction[] PaymentInstructions { get; set; }

        [DataMember(Name = "delivery", EmitDefaultValue = false)]
        public DeliveryOption[] DeliveryOptions { get; set; }

        [DataMember(Name = "pickup", EmitDefaultValue = false)]
        public PickupOption<string[]>[] PickupOptions { get; set; }

        [DataMember(Name = "webhook_subscriptions", EmitDefaultValue = false)]
        public Subscription[] WebhookSubscriptions { get; set; }

        [DataMember(Name = "hooks", EmitDefaultValue = false)]
        public ShipmentStatusChangeEvent[] Webhooks { get; set; }

        [DataMember(Name = "download_url", EmitDefaultValue = false)]
        public DownloadUrl DownloadUrl { get; set; }

        [DataMember(Name = "pdfs", EmitDefaultValue = false)]
        public ShipmentLabelDownloadLink DownloadLink { get; set; }

        [DataMember(Name = "results", EmitDefaultValue = false)]
        public int Results { get; set; }

        [DataMember(Name = "page", EmitDefaultValue = false)]
        public int Page { get; set; }

        [DataMember(Name = "size", EmitDefaultValue = false)]
        public int Size { get; set; }

        [DataMember(Name = "message", EmitDefaultValue = false)]
        public string Message { get; set; }

        [DataMember(Name = "deliveries", EmitDefaultValue = false)]
        public Delivery[] Deliveries { get; set; }

        [DataMember(Name = "pickup_locations", EmitDefaultValue = false)]
        public PickupLocation[] PickupLocations { get; set; }
    }
}
