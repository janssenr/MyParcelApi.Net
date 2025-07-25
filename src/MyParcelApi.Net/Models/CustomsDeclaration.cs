using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class CustomsDeclaration
    {
        [DataMember(Name = "contents", EmitDefaultValue = false, IsRequired = true)]
        public PackageContents Contents { get; set; }

        [DataMember(Name = "invoice", EmitDefaultValue = false, IsRequired = false)]
        public string Invoice { get; set; }

        [DataMember(Name = "weight", EmitDefaultValue = true, IsRequired = false)]
        public int Weight { get; set; }

        [DataMember(Name = "items", EmitDefaultValue = false, IsRequired = true)]
        public CustomsItem[] Items { get; set; }
    }
}
