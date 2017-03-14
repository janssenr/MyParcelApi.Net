using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using MyParcelApi.Net;
using MyParcelApi.Net.Models;
using NUnit.Framework;

namespace MyParcelApi.Tests
{
    [TestFixture]
    public class WebhooksTests
    {
        private MyParcelApiClient _client;

        [SetUp]
        public void SetUp()
        {
            var apiKey = ConfigurationManager.AppSettings["ApiKey"];
            _client = new MyParcelApiClient(apiKey);
        }

        [Test]
        public void CreateSubscription()
        {
            var conceptSubscriptions = new List<Subscription>
            {
                new Subscription
                {
                    Hook = "shipment_status_change",
                    Url = "https://seoshop.nl/myparcel/notifications"
                }
            };
            var response = _client.AddSubscription(conceptSubscriptions.ToArray());
            response.Wait();
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Result.Length > 0);

            var subscriptionIds = response.Result.Select(obj => obj.Id).ToArray();

            ValidateSubscription(subscriptionIds, conceptSubscriptions);

            DeleteSubscription(subscriptionIds);
        }

        private void ValidateSubscription(int[] subscriptionIds, List<Subscription> conceptSubscriptions)
        {
            var subscriptions = _client.GetSubscription(subscriptionIds);
            subscriptions.Wait();
            Assert.IsNotNull(subscriptions);
            Assert.IsTrue(subscriptions.Result.Length > 0);
            foreach (var subscription in subscriptions.Result)
            {
                var index = subscriptionIds.ToList().IndexOf(subscriptionIds.FirstOrDefault(obj => obj == subscription.Id));
                if (index >= 0)
                {
                    var conceptSubscription = conceptSubscriptions[index];
                    Assert.AreEqual(conceptSubscription.Hook, subscription.Hook);
                    Assert.AreEqual(conceptSubscription.Url, subscription.Url);
                }
            }
        }

        private void DeleteSubscription(int[] subscriptionIds)
        {
            var result = _client.DeleteSubscription(subscriptionIds);
            result.Wait();
        }
    }
}
