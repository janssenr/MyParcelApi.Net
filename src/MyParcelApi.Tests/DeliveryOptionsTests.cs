using System;
using System.Configuration;
using MyParcelApi.Net;
using MyParcelApi.Net.Models;
using NUnit.Framework;

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
            _client = new MyParcelApiClient(apiKey);
        }

        [Test]
        public void GetDeliveryOptionsForPostalCodeAndNumberAndCarrier()
        {
            var response = _client.GetDeliveryOptions("NL", "2132WT", "66", Carrier.PostNl);
            response.Wait();
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);
            Assert.IsTrue(response.Result.DeliveryOptions.Length > 0);
            Assert.IsTrue(response.Result.PickupOptions.Length > 0);
        }

        [Test]
        public void GetDeliveryOptionsForPostalCodeAndNumberAndCarrierAndCutoffTimeAndDropoffDaysAndDropoffDelayAndDeliverydaysWindowAndDeliveryType()
        {
            var response = _client.GetDeliveryOptions("NL", "2132WT", "66", Carrier.PostNl, cutoffTime: new TimeSpan(16, 0, 0), dropoffDays: new [] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday }, dropoffDelay: 0, deliverydaysWindow: 2, excludeDeliveryType: new[] { DeliveryType.Morning });
            response.Wait();
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);
            Assert.IsTrue(response.Result.DeliveryOptions.Length > 0);
            Assert.IsTrue(response.Result.PickupOptions.Length > 0);
        }
    }
}
