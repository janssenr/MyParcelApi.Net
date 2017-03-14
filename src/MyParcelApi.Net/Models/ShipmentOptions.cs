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

        [IgnoreDataMember]
        public bool? OnlyRecipient {
            get
            {
                if (OnlyRecipientRaw.HasValue)
                {
                    return Convert.ToBoolean(OnlyRecipientRaw);
                }
                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    OnlyRecipientRaw = Convert.ToInt32(value);
                }
                else
                {
                    OnlyRecipientRaw = null;
                }
            }
        }
        [DataMember(Name = "only_recipient", EmitDefaultValue = false, IsRequired = false)]
        public int? OnlyRecipientRaw { get; private set; }

        [IgnoreDataMember]
        public bool? Signature
        {
            get
            {
                if (SignatureRaw.HasValue)
                {
                    return Convert.ToBoolean(SignatureRaw);
                }
                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    SignatureRaw = Convert.ToInt32(value);
                }
                else
                {
                    SignatureRaw = null;
                }
            }
        }
        [DataMember(Name = "signature", EmitDefaultValue = false, IsRequired = false)]
        public int? SignatureRaw { get; private set; }

        [IgnoreDataMember]
        public bool? Return
        {
            get
            {
                if (ReturnRaw.HasValue)
                {
                    return Convert.ToBoolean(ReturnRaw);
                }
                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    ReturnRaw = Convert.ToInt32(value);
                }
                else
                {
                    ReturnRaw = null;
                }
            }
        }
        [DataMember(Name = "return", EmitDefaultValue = false, IsRequired = false)]
        public int? ReturnRaw { get; set; }

        [DataMember(Name = "insurance", EmitDefaultValue = false, IsRequired = false)]
        public Price Insurance { get; set; }

        [IgnoreDataMember]
        public bool? LargeFormat
        {
            get
            {
                if (LargeFormatRaw.HasValue)
                {
                    return Convert.ToBoolean(LargeFormatRaw);
                }
                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    LargeFormatRaw = Convert.ToInt32(value);
                }
                else
                {
                    LargeFormatRaw = null;
                }
            }
        }
        [DataMember(Name = "large_format", EmitDefaultValue = false, IsRequired = false)]
        public int? LargeFormatRaw { get; set; }

        [IgnoreDataMember]
        public bool? CooledDelivery
        {
            get
            {
                if (CooledDeliveryRaw.HasValue)
                {
                    return Convert.ToBoolean(CooledDeliveryRaw);
                }
                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    CooledDeliveryRaw = Convert.ToInt32(value);
                }
                else
                {
                    CooledDeliveryRaw = null;
                }
            }
        }
        [DataMember(Name = "cooled_delivery", EmitDefaultValue = false, IsRequired = false)]
        public int? CooledDeliveryRaw { get; set; }

        [DataMember(Name = "label_description", EmitDefaultValue = false, IsRequired = false)]
        public string LabelDescription { get; set; }
    }
}
