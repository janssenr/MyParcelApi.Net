using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class PhysicalProperties
    {
        [DataMember(Name = "carrier_height", EmitDefaultValue = false)]
        public int? CarrierHeight { get; set; }

        [DataMember(Name = "carrier_width", EmitDefaultValue = false)]
        public int? CarrierWidth { get; set; }

        [DataMember(Name = "carrier_weight", EmitDefaultValue = false)]
        public int? CarrierWeight { get; set; }

        [DataMember(Name = "carrier_length", EmitDefaultValue = false)]
        public int? CarrierLength { get; set; }

        [DataMember(Name = "carrier_volume", EmitDefaultValue = false)]
        public int? CarrierVolume { get; set; }

        [DataMember(Name = "height", EmitDefaultValue = false)]
        public int? Height { get; set; }

        [DataMember(Name = "width", EmitDefaultValue = false)]
        public int? Width { get; set; }

        [DataMember(Name = "length", EmitDefaultValue = false)]
        public int? Length { get; set; }

        [DataMember(Name = "volume", EmitDefaultValue = false)]
        public int? Volume { get; set; }

        [DataMember(Name = "weight", EmitDefaultValue = true)]
        public int? Weight { get; set; }
    }
}
