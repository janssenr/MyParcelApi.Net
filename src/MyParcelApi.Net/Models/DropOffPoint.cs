using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyParcelApi.Net.Models
{
	[DataContract]
	public class DropOffPoint
	{
		[DataMember(Name = "location_code", EmitDefaultValue = false)]
		public string LocationCode { get; set; }

		[DataMember(Name = "location_name", EmitDefaultValue = false)]
		public string LocationName { get; set; }

		[DataMember(Name = "cc", EmitDefaultValue = false)]
		public string CountryCode { get; set; }

		[DataMember(Name = "state", EmitDefaultValue = false)]
		public string State { get; set; }

		[DataMember(Name = "city", EmitDefaultValue = false)]
		public string City { get; set; }

		[DataMember(Name = "postal_code", EmitDefaultValue = false)]
		public string PostalCode { get; set; }

		[DataMember(Name = "street", EmitDefaultValue = false)]
		public string Street { get; set; }

		[DataMember(Name = "number", EmitDefaultValue = false)]
		public string Number { get; set; }

		[DataMember(Name = "number_suffix", EmitDefaultValue = false)]
		public string NumberSuffix { get; set; }

		[DataMember(Name = "phone", EmitDefaultValue = false)]
		public string Phone { get; set; }

		[DataMember(Name = "reference", EmitDefaultValue = false)]
		public string Reference { get; set; }

		[DataMember(Name = "longitude", EmitDefaultValue = false)]
		public double Longitude { get; set; }

		[DataMember(Name = "latitude", EmitDefaultValue = false)]
		public double Latitude { get; set; }

		[DataMember(Name = "available_days", EmitDefaultValue = false)]
		public int[] AvailableDays { get; set; }

		[DataMember(Name = "cut_off_time", EmitDefaultValue = false)]
		public string CutOffTime { get; set; }

		[DataMember(Name = "carrier", EmitDefaultValue = false)]
		public Carrier Carrier { get; set; }

		[DataMember(Name = "distance", EmitDefaultValue = false)]
		public int Distance { get; set; }

		[DataMember(Name = "occupancy", EmitDefaultValue = false)]
		public string Occupancy { get; set; }

		[DataMember(Name = "occupancy_today", EmitDefaultValue = false)]
		public string OccupancyToday { get; set; }

		[DataMember(Name = "default_drop_off_point", EmitDefaultValue = false)]
		public string DefaultDropOffPoint { get; set; }

		[DataMember(Name = "opening_hours", EmitDefaultValue = false)]
		public OpeningHours<Moment[]> OpeningHours { get; set; }
	}
}
