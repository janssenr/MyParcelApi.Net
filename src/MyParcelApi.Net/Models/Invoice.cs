using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class Invoice
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public int Id { get; set; }

        [DataMember(Name = "number", EmitDefaultValue = false)]
        public string Number { get; set; }

        [DataMember(Name = "status", EmitDefaultValue = false)]
        public int Status { get; set; }
    }
}
