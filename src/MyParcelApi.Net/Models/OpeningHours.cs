using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class OpeningHours
    {
        [DataMember(Name = "monday", EmitDefaultValue = false)]
        public string[] Monday { get; set; }

        [DataMember(Name = "tuesday", EmitDefaultValue = false)]
        public string[] Tuesday { get; set; }

        [DataMember(Name = "wednesday", EmitDefaultValue = false)]
        public string[] Wednesday { get; set; }

        [DataMember(Name = "thursday", EmitDefaultValue = false)]
        public string[] Thursday { get; set; }

        [DataMember(Name = "friday", EmitDefaultValue = false)]
        public string[] Friday { get; set; }

        [DataMember(Name = "saturday", EmitDefaultValue = false)]
        public string[] Saturday { get; set; }

        [DataMember(Name = "sunday", EmitDefaultValue = false)]
        public string[] Sunday { get; set; }
    }
}
