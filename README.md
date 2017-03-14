# MyParcelApi.Net [![Build status](https://ci.appveyor.com/api/projects/status/heqg2l4f1mqtlrr0?svg=true)](https://ci.appveyor.com/project/janssenr/myparcelapi-net) [![NuGet version](https://badge.fury.io/nu/myparcelapi.svg)](https://badge.fury.io/for/nu/myparcelapi)
A C#/.net wrapper for the MyParce.nl API

#Installation

MyParcelApi.Net can easily be installed using the NuGet package

```
Install-Package MyParcelApi
```


# Getting started
Just create a new `MyParcelApiClient` with your own API key.
API keys are available on [MyParcel.nl](https://www.myparcel.nl/) check the API documentation [here](https://myparcelnl.github.io/api/)

```
var client = new MyParcelApiClient("FILL IN KEY HERE");
```

## AddShipment
Add shipments allows you to create standard shipments.

```
var shipments = new List<Shipment>
{
	new Shipment
	{
		Recipient = new Address
		{
			Country = "NL",
			City = "Hoofddorp",
			Street = "Siriusdreef",
			Number = "66",
			PostalCode = "2132WT",
			Person = "Mr. Parcel",
			Phone = "0213030315",
			Email = "testing@myparcel.nl"
		},
		Options = new ShipmentOptions
		{
			PackageType = PackageType.Package,
			OnlyRecipient = true,
			Signature = true,
			Return = true,
			Insurance = new Price
			{
				Amount = 50000,
				Currency = "EUR"
			},
			LargeFormat = false
		},
		Carrier = Carrier.PostNl
	},
	new Shipment
	{
		Recipient = new Address
		{
			Country = "NL",
			City = "Amsterdam",
			Street = "Dorpstraat",
			Number = "123",
			PostalCode = "1020BC",
			Person = "Mrs. Parcel",
			Phone = "02012343546",
			Email = "info@myparcel.nl"
		},
		Options = new ShipmentOptions
		{
			PackageType = PackageType.Package,
			OnlyRecipient = false,
			Signature = false,
			Return = false
		},
		Carrier = Carrier.PostNl
	}
};
var shipmentIds = await client.AddShipment(shipments.ToArray());
foreach (var shipmentId in shipmentIds)
{
	Console.WriteLine(shipmentId.Id);
}
```

## AddReturnShipment
Add return shipments allows you to create related return shipments.

```
var returnShipments = new List<ReturnShipment>
{
	new ReturnShipment
	{
		Parent = 5,
		Carrier = Carrier.PostNl,
		Email = "testing@myparcel",
		Name = "Mr. Parcel",
		Options = new ShipmentOptions
		{
			PackageType = PackageType.Package,
			OnlyRecipient = false,
			Signature = true,
			Return = false,
			Insurance = new Price
			{
				Amount = 50,
				Currency = "EUR"
			}
		}
	}
};
var shipmentIds = await client.AddReturnShipment(returnShipments.ToArray());
foreach (var shipmentId in shipmentIds)
{
	Console.WriteLine(shipmentId.Id);
}
```

## AddUnrelatedReturnShipment
Add unrelated return shipments allows you to create unrelated return shipments.

```
var returnShipments = new List<ReturnShipment>
{
	new ReturnShipment
	{
		Carrier = Carrier.PostNl,
		Email = "testing@myparcel",
		Name = "Test",
		Options = new ShipmentOptions
		{
			PackageType = PackageType.Package,
			OnlyRecipient = false,
			Signature = false,
			Return = false,
			Insurance = new Price
			{
				Amount = 0,
				Currency = "EUR"
			},
			LargeFormat = false,
			CooledDelivery = true,
			LabelDescription = "test"
		}
	}
};
var shipmentIds = await client.AddUnrelatedReturnShipment(returnShipments.ToArray());
foreach (var shipmentId in shipmentIds)
{
	Console.WriteLine(shipmentId.Id);
}
```

## DeleteShipment
Use this function to remove shipments. You can specify multiple shipment ids. Only shipments with status 'pending - concept' can be deleted. 

```
var success = await client.DeleteShipment(new[] { 1234 });
Console.WriteLine(success);
```

## GenerateUnrelatedReturnShipment
This function is for external parties to facilitate return shipments on a dedicated part of their website, mainly when offering reverse logistics e.g. repair services. It will allow the consumer to send packages to the merchant directly from the merchant's website.

```
var downloadUrl = await client.GenerateUnrelatedReturnShipment();
Console.WriteLine(downloadUrl.Link);
```

## GetShipment
With this function you can get shipments. You can use the 'q' query parameter to search for shipments. Multiple shipment ids can be specified.

```
var shipments = await client.GetShipment();
foreach (var shipment in shipments)
{
	Console.WriteLine(shipment.Id);
	Console.WriteLine(shipment.Status);
	Console.WriteLine(shipment.ShipmentType);
	Console.WriteLine(shipment.Barcode);
}
```

## GetShipmentLabel
Get shipment label. You can specify label format and starting position of labels on the first page with the FORMAT and POSITION query parameters. The POSITION parameter only works when you specify the A4 format and is only applied on the first page with labels. 

```
using (var stream = await client.GetShipmentLabel(new[] { 12 }, "A4", new[] { 3, 4 }))
{
	using (var fs = new FileStream(@"C:\Temp\test.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
	{
		await stream.CopyToAsync(fs);
	}
}
```

## GetShipmentLabelDownloadLink
Get shipment label. You can specify label format and starting position of labels on the first page with the FORMAT and POSITION query parameters. The POSITION parameter only works when you specify the A4 format and is only applied on the first page with labels. 

```
var downloadLink = await client.GetShipmentLabelDownloadLink(new[] { 12 });
Console.WriteLine(downloadLink.Url);
```

## TrackShipment
Get detailed track and trace information for a shipment.

```
var trackTraces = await client.TrackShipment(new[] { 12 });
foreach (var trackTrace in trackTraces)
{
	Console.WriteLine(trackTrace.ShipmentId);
	Console.WriteLine(trackTrace.Code);
}
```

## GetDeliveryOptions
Get the delivery options for a given location and carrier. If none of the optional parameters are specified then the following default will be used: If a request is made for the delivery options between Friday after the default cutoff_time (15h30) and Monday before the default cutoff_time (15h30) then Tuesday will be shown as the next possible delivery date.

```
var deliveryOptions = await client.GetDeliveryOptions("NL", "2132WT", "66", Carrier.PostNl);
foreach (var deliveryOption in deliveryOptions.DeliveryOptions)
{
	Console.WriteLine(deliveryOption.Date);
}
foreach (var pickupOption in deliveryOptions.PickupOptions)
{
	Console.WriteLine(pickupOption.Date);
	Console.WriteLine(pickupOption.Location);
}
```

## AddSubscription
Use this function to subscribe to an event in the API. 

```
var subscriptions = new List<Subscription>
{
	new Subscription
	{
	    Hook = "shipment_status_change",
	    Url = "https://seoshop.nl/myparcel/notifications"
	}
};
var subscriptionIds = await client.AddSubscription(subscriptions.ToArray());
foreach (var subscriptionId in subscriptionIds)
{
	Console.WriteLine(subscriptionId.Id);
}
```

## DeleteSubscription
Use this function to delete webhook subscriptions. You specify multiple subscription ids 

```
var success = await client.DeleteSubscription(new[] { 1 });
Console.WriteLine(success);
```

## GetSubscription
Use this function to fetch webhook subscriptions. You can also filter by account and shop id that you have access to.

```
var susbcriptions = await client.GetSubscription(new[] { 1 });
foreach (var subscription in susbcriptions)
{
	Console.WriteLine(subscription.Id);
	Console.WriteLine(subscription.AccountId);
	Console.WriteLine(subscription.ShopId);
	Console.WriteLine(subscription.Hook);
	Console.WriteLine(subscription.Url);
}
```
