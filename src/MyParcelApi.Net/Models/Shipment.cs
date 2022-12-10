using System;
using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class Shipment
    {
        [DataMember(Name = "id", EmitDefaultValue = false, IsRequired = false)]
        public int Id { get; set; }

        [DataMember(Name = "parent_id", EmitDefaultValue = false, IsRequired = false)]
        public int? ParentId { get; set; }

        [DataMember(Name = "account_id", EmitDefaultValue = false, IsRequired = false)]
        public int AccountId { get; set; }

        [DataMember(Name = "shop_id", EmitDefaultValue = false, IsRequired = false)]
        public int ShopId { get; set; }

        [DataMember(Name = "shipment_type", EmitDefaultValue = false, IsRequired = false)]
        public int ShipmentType { get; set; }

        [DataMember(Name = "recipient", EmitDefaultValue = false, IsRequired = false)]
        public Address Recipient { get; set; }

        [DataMember(Name = "sender", EmitDefaultValue = false)]
        public Address Sender { get; private set; }

        [DataMember(Name = "status", EmitDefaultValue = false, IsRequired = false)]
        public ShipmentStatus Status { get; set; }

        [DataMember(Name = "options", EmitDefaultValue = false, IsRequired = false)]
        public ShipmentOptions Options { get; set; }

        [DataMember(Name = "general_settings", EmitDefaultValue = false, IsRequired = false)]
        public ShipmentGeneralSettings GeneralSettings { get; set; }

        [DataMember(Name = "pickup", EmitDefaultValue = false, IsRequired = false)]
        public ShipmentPickupLocation Pickup { get; set; }

        [DataMember(Name = "customs_declaration", EmitDefaultValue = false, IsRequired = false)]
        public CustomsDeclaration CustomsDeclaration { get; set; }

        [DataMember(Name = "physical_properties", EmitDefaultValue = false, IsRequired = false)]
        public PhysicalProperties PhysicalProperties { get; set; }

        [DataMember(Name = "carrier", EmitDefaultValue = false, IsRequired = false)]
        public Carrier Carrier { get; set; }

        /// <summary>
        /// This is a workarround for a bug in the MyParcelApi where there is inconsistency between getting the Carriers [GET] and Creating/Updating them [POST/PUT].
        /// It's also possible to be null
        /// </summary>
        [DataMember(Name = "carrier_id", EmitDefaultValue = false, IsRequired = false)]
        private Carrier? CarrierFallback
        {
            get
            {
                //return Carrier;
                //Set to null, because of error "The property carrier_id is not defined and the definition does not allow additional properties."
                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    Carrier = value.Value;
                }

            }
        }

        [DataMember(Name = "created", EmitDefaultValue = false)]
        public DateTime Created { get; set; }

        [DataMember(Name = "modified", EmitDefaultValue = false)]
        public DateTime Modified { get; set; }

        [DataMember(Name = "reference_identifier", EmitDefaultValue = false)]
        public string ReferenceIdentifier { get; set; }

        [DataMember(Name = "created_by", EmitDefaultValue = false)]
        public string CreatedBy { get; set; }

        [DataMember(Name = "modified_by", EmitDefaultValue = false)]
        public string ModifiedBy { get; set; }

        [DataMember(Name = "transaction_status", EmitDefaultValue = false)]
        public string TransactionStatus { get; set; }

        [DataMember(Name = "barcode", EmitDefaultValue = false)]
        public string Barcode { get; set; }

        [DataMember(Name = "price", EmitDefaultValue = false)]
        public Price Price { get; set; }

        [DataMember(Name = "region", EmitDefaultValue = false)]
        public string Region { get; set; }

        [DataMember(Name = "payment_status", EmitDefaultValue = false)]
        public string PaymentStatus { get; set; }

        [DataMember(Name = "secondary_shipments", EmitDefaultValue = false)]
        public Shipment[] SecondaryShipments { get; set; }

        //[DataMember(Name = "collection_contact", EmitDefaultValue = false)]
        //public string CollectionContact { get; set; }

        //[DataMember(Name = "multi_collo_main_shipment_id", EmitDefaultValue = false)]
        //public int? MultiColloMainShipmentId { get; set; }

        [DataMember(Name = "external_identifier", EmitDefaultValue = false)]
        public string ExternalIdentifier { get; set; }

        [DataMember(Name = "delayed", EmitDefaultValue = false)]
        public bool Delayed { get; set; }

        [DataMember(Name = "delivered", EmitDefaultValue = false)]
        public bool Delivered { get; set; }

        [DataMember(Name = "link_consumer_portal", EmitDefaultValue = false)]
        public string LinkConsumerPortal { get; set; }

        //[DataMember(Name = "partner_tracktraces", EmitDefaultValue = false)]
        //public TrackTrace[] PartnerTracktraces { get; set; }
    }
}
