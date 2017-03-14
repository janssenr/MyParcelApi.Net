using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class Subscription
    {
        [DataMember(Name = "id", EmitDefaultValue = false, IsRequired = false)]
        public int Id { get; set; }

        [DataMember(Name = "hook", EmitDefaultValue = false, IsRequired = true)]
        public string Hook { get; set; }

        [DataMember(Name = "url", EmitDefaultValue = false, IsRequired = true)]
        public string Url { get; set; }

        [DataMember(Name = "account_id", EmitDefaultValue = false, IsRequired = false)]
        public int AccountId { get; set; }

        [DataMember(Name = "shop_id", EmitDefaultValue = false, IsRequired = false)]
        public int ShopId { get; set; }
    }
}
