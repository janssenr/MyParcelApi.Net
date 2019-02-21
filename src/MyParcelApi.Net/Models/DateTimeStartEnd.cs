using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class DateTimeStartEnd
    {
        [DataMember(Name = "date", EmitDefaultValue = false)]
        public string DateRaw { get; set; }
        public DateTime Date
        {
            get => DateTime.ParseExact(DateRaw, "yyyy-MM-dd HH:mm:ss.ffffff", CultureInfo.InvariantCulture);
            set => DateRaw = value.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
        }

        [DataMember(Name = "timezone_type", EmitDefaultValue = false)]
        public int TimezoneType { get; set; }

        [DataMember(Name = "timezone", EmitDefaultValue = false)]
        public string Timezone { get; set; }
    }
}
