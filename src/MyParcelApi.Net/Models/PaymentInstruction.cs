using System;
using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class PaymentInstruction
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        [DataMember(Name = "hash", EmitDefaultValue = false)]
        public string Hash { get; set; }

        [DataMember(Name = "invoices", EmitDefaultValue = false)]
        public Invoice Invoice { get; set; }

        [DataMember(Name = "type", EmitDefaultValue = false)]
        public int Type { get; set; }

        [DataMember(Name = "notification_hash", EmitDefaultValue = false)]
        public string NotificationHash { get; set; }

        [DataMember(Name = "original_notification_hash", EmitDefaultValue = false)]
        public string OriginalNotificationHash { get; set; }

        [DataMember(Name = "payment_url", EmitDefaultValue = false)]
        public string PaymentUrl { get; set; }

        [DataMember(Name = "paid", EmitDefaultValue = false)]
        private int _paidRaw;
        [IgnoreDataMember]
        public bool Paid
        {
            get
            {
                return Convert.ToBoolean(_paidRaw);
            }
            set
            {
                _paidRaw = Convert.ToInt32(value);
            }
        }

        [DataMember(Name = "price", EmitDefaultValue = false)]
        public Price Price { get; set; }
    }
}
