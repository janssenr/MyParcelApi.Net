using System;
using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class OpeningHours<T>
    {
        [DataMember(Name = "monday", EmitDefaultValue = false)]
        public T Monday { get; set; }

        [DataMember(Name = "tuesday", EmitDefaultValue = false)]
        public T Tuesday { get; set; }

        [DataMember(Name = "wednesday", EmitDefaultValue = false)]
        public T Wednesday { get; set; }

        [DataMember(Name = "thursday", EmitDefaultValue = false)]
        public T Thursday { get; set; }

        [DataMember(Name = "friday", EmitDefaultValue = false)]
        public T Friday { get; set; }

        [DataMember(Name = "saturday", EmitDefaultValue = false)]
        public T Saturday { get; set; }

        [DataMember(Name = "sunday", EmitDefaultValue = false)]
        public T Sunday { get; set; }
    }
}
