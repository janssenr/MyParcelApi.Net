using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class DownloadUrl
    {
        [DataMember(Name = "link", EmitDefaultValue = false)]
        public string Link { get; set; }
    }
}
