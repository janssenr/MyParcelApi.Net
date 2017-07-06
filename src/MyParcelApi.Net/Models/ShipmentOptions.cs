using System;
using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class ShipmentOptions
    {
        [DataMember(Name = "package_type", EmitDefaultValue = false, IsRequired = false)]
        public PackageType PackageType { get; set; }

        [DataMember(Name = "delivery_type", EmitDefaultValue = false, IsRequired = false)]
        public DeliveryType DeliveryType { get; set; }

        [DataMember(Name = "delivery_date", EmitDefaultValue = false, IsRequired = false)]
        public DateTime DeliveryDate { get; set; }

        [DataMember(Name = "delivery_remark", EmitDefaultValue = false, IsRequired = false)]
        public string DeliveryRemark { get; set; }

        [DataMember(Name = "only_recipient", EmitDefaultValue = false, IsRequired = false)]
        private int? _onlyRecipientRaw;
        [IgnoreDataMember]
        public bool? OnlyRecipient {
            get
            {
                if (_onlyRecipientRaw.HasValue)
                {
                    return Convert.ToBoolean(_onlyRecipientRaw);
                }
                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    _onlyRecipientRaw = Convert.ToInt32(value);
                }
                else
                {
                    _onlyRecipientRaw = null;
                }
            }
        }

        [DataMember(Name = "signature", EmitDefaultValue = false, IsRequired = false)]
        private int? _signatureRaw;
        [IgnoreDataMember]
        public bool? Signature
        {
            get
            {
                if (_signatureRaw.HasValue)
                {
                    return Convert.ToBoolean(_signatureRaw);
                }
                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    _signatureRaw = Convert.ToInt32(value);
                }
                else
                {
                    _signatureRaw = null;
                }
            }
        }

        [DataMember(Name = "return", EmitDefaultValue = false, IsRequired = false)]
        private int? _returnRaw;
        [IgnoreDataMember]
        public bool? Return
        {
            get
            {
                if (_returnRaw.HasValue)
                {
                    return Convert.ToBoolean(_returnRaw);
                }
                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    _returnRaw = Convert.ToInt32(value);
                }
                else
                {
                    _returnRaw = null;
                }
            }
        }

        [DataMember(Name = "insurance", EmitDefaultValue = false, IsRequired = false)]
        public Price Insurance { get; set; }

        [DataMember(Name = "large_format", EmitDefaultValue = false, IsRequired = false)]
        private int? _largeFormatRaw;
        [IgnoreDataMember]
        public bool? LargeFormat
        {
            get
            {
                if (_largeFormatRaw.HasValue)
                {
                    return Convert.ToBoolean(_largeFormatRaw);
                }
                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    _largeFormatRaw = Convert.ToInt32(value);
                }
                else
                {
                    _largeFormatRaw = null;
                }
            }
        }

        [DataMember(Name = "cooled_delivery", EmitDefaultValue = false, IsRequired = false)]
        private int? _cooledDeliveryRaw;
        [IgnoreDataMember]
        public bool? CooledDelivery
        {
            get
            {
                if (_cooledDeliveryRaw.HasValue)
                {
                    return Convert.ToBoolean(_cooledDeliveryRaw);
                }
                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    _cooledDeliveryRaw = Convert.ToInt32(value);
                }
                else
                {
                    _cooledDeliveryRaw = null;
                }
            }
        }

        [DataMember(Name = "label_description", EmitDefaultValue = false, IsRequired = false)]
        public string LabelDescription { get; set; }
    }
}
