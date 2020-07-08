using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class Location<T>
    {
        [DataMember(Name = "date", EmitDefaultValue = false)]
        public string Date { get; set; }

        [DataMember(Name = "time", EmitDefaultValue = false)]
        public LocationTime[] Times { get; set; }

        [DataMember(Name = "location", EmitDefaultValue = false)]
        private string LocationNameOld
        {
            get
            {
                return LocationName;
            }
            set
            {
                LocationName = value;
            }
        }

        [DataMember(Name = "location_name", EmitDefaultValue = false)]
        public string LocationName { get; set; }

        [DataMember(Name = "location_code", EmitDefaultValue = false)]
        public string LocationCode { get; set; }

        [DataMember(Name = "retail_network_id", EmitDefaultValue = false)]
        public string RetailNetworkId { get; set; }

        [DataMember(Name = "street", EmitDefaultValue = false)]
        public string Street { get; set; }

        [DataMember(Name = "number", EmitDefaultValue = false)]
        public string Number { get; set; }

        [DataMember(Name = "postal_code", EmitDefaultValue = false)]
        public string PostalCode { get; set; }

        [DataMember(Name = "city", EmitDefaultValue = false, IsRequired = false)]
        public string City { get; set; }

        [DataMember(Name = "cc", EmitDefaultValue = false, IsRequired = false)]
        public string Country { get; set; }

        [DataMember(Name = "start_time", EmitDefaultValue = false)]
        public string StartTime { get; set; }

        [DataMember(Name = "price", EmitDefaultValue = false)]
        public int Price { get; set; }

        [DataMember(Name = "price_comment", EmitDefaultValue = false)]
        public string PriceComment { get; set; }

        [DataMember(Name = "comment", EmitDefaultValue = false)]
        public string Comment { get; set; }

        [DataMember(Name = "phone_number", EmitDefaultValue = false)]
        public string PhoneNumber { get; set; }

        [DataMember(Name = "opening_hours", EmitDefaultValue = false)]
        public OpeningHours<T> OpeningHours { get; set; }

        [DataMember(Name = "distance", EmitDefaultValue = false)]
        public string Distance { get; set; }

        [DataMember(Name = "latitude", EmitDefaultValue = false)]
        public string Latitude { get; set; }

        [DataMember(Name = "longitude", EmitDefaultValue = false)]
        public string Longitude { get; set; }
    }
}
