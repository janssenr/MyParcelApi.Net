using System;
using System.Globalization;
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

        [DataMember(Name = "delivery_date", EmitDefaultValue = false, IsRequired = false, Order = 2)]
        public string DeliveryDateRaw { get; set; }
        public DateTime? DeliveryDate
        {
            get => !string.IsNullOrWhiteSpace(DeliveryDateRaw) ? DateTime.ParseExact(DeliveryDateRaw, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) : (System.DateTime?)null;
            set => DeliveryDateRaw = value?.ToString("yyyy-MM-dd HH:mm:ss");
        }

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

        [DataMember(Name = "age_check", EmitDefaultValue = false, IsRequired = false)]
        private int? _ageCheckRaw;
        [IgnoreDataMember]
        public bool? AgeCheck
        {
            get
            {
                if (_ageCheckRaw.HasValue)
                {
                    return Convert.ToBoolean(_ageCheckRaw);
                }
                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    _ageCheckRaw = Convert.ToInt32(value);
                }
                else
                {
                    _ageCheckRaw = null;
                }
            }
        }

		[DataMember(Name = "receipt_code", EmitDefaultValue = false, IsRequired = false)]
		private int? _receiptCode;
		[IgnoreDataMember]
		public bool? ReceiptCode
		{
			get
			{
				if (_receiptCode.HasValue)
				{
					return Convert.ToBoolean(_receiptCode);
				}
				return null;
			}
			set
			{
				if (value.HasValue)
				{
					_receiptCode = Convert.ToInt32(value);
				}
				else
				{
					_receiptCode = null;
				}
			}
		}

		[DataMember(Name = "saturday_delivery", EmitDefaultValue = false, IsRequired = false)]
		private int? _saturdayDelivery;
		[IgnoreDataMember]
		public bool? SaturdayDelivery
		{
			get
			{
				if (_saturdayDelivery.HasValue)
				{
					return Convert.ToBoolean(_saturdayDelivery);
				}
				return null;
			}
			set
			{
				if (value.HasValue)
				{
					_saturdayDelivery = Convert.ToInt32(value);
				}
				else
				{
					_saturdayDelivery = null;
				}
			}
		}

		[DataMember(Name = "fresh_food", EmitDefaultValue = false, IsRequired = false)]
		private int? _freshFood;
		[IgnoreDataMember]
		public bool? FreshFood
		{
			get
			{
				if (_freshFood.HasValue)
				{
					return Convert.ToBoolean(_freshFood);
				}
				return null;
			}
			set
			{
				if (value.HasValue)
				{
					_freshFood = Convert.ToInt32(value);
				}
				else
				{
					_freshFood = null;
				}
			}
		}

		[DataMember(Name = "frozen", EmitDefaultValue = false, IsRequired = false)]
		private int? _frozen;
		[IgnoreDataMember]
		public bool? Frozen
		{
			get
			{
				if (_frozen.HasValue)
				{
					return Convert.ToBoolean(_frozen);
				}
				return null;
			}
			set
			{
				if (value.HasValue)
				{
					_frozen = Convert.ToInt32(value);
				}
				else
				{
					_frozen = null;
				}
			}
		}
	}
}
