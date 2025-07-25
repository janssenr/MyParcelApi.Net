using System;
using System.Configuration;
using MyParcelApi.Net;
using MyParcelApi.Net.Models;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace MyParcelApi.Tests
{
	[TestFixture]
	public class DeliveryOptionsTests
	{
		private MyParcelApiClient _client;

		[SetUp]
		public void SetUp()
		{
			var apiKey = ConfigurationManager.AppSettings["ApiKey"];
			if (string.IsNullOrEmpty(apiKey)) apiKey = Environment.GetEnvironmentVariable("ApiKey");
			_client = new MyParcelApiClient(apiKey);
		}

		[Test]
		public void GetDeliveryOptionsForPostalCodeAndNumberAndCarrier()
		{
			var response = _client.GetDeliveryOptions("NL", "2132WT", "66", Platform.MyParcel, Carrier.PostNl);
			response.Wait();
			ClassicAssert.IsNotNull(response);
			ClassicAssert.IsNotNull(response.Result);
			ClassicAssert.IsTrue(response.Result.DeliveryOptions.Length > 0);
			ClassicAssert.IsTrue(response.Result.PickupOptions.Length > 0);
		}

		[Test]
		public void GetDeliveryOptionsForPostalCodeAndNumberAndCarrierAndCutoffTimeAndDropoffDaysAndDropoffDelayAndDeliverydaysWindowAndDeliveryType()
		{
			var response = _client.GetDeliveryOptions("NL", "2132WT", "66", Platform.MyParcel, Carrier.PostNl, cutoffTime: new TimeSpan(16, 0, 0), dropoffDays: new[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday }, dropoffDelay: 0, deliverydaysWindow: 2, excludeDeliveryType: new[] { DeliveryType.Morning });
			response.Wait();
			ClassicAssert.IsNotNull(response);
			ClassicAssert.IsNotNull(response.Result);
			ClassicAssert.IsTrue(response.Result.DeliveryOptions.Length > 0);
			ClassicAssert.IsTrue(response.Result.PickupOptions.Length > 0);
		}
	}
}
