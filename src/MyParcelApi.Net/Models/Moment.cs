using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class Moment
    {
        [DataMember(Name = "start", EmitDefaultValue = false)]
        public DateTimeStartEnd Start { get; set; }

        [DataMember(Name = "end", EmitDefaultValue = false)]
        public DateTimeStartEnd End { get; set; }
    }
}
