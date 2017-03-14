using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class ObjectId
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public int Id { get; set; }
    }
}
