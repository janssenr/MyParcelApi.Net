using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using MyParcelApi.Net;
using MyParcelApi.Net.Models;
using NUnit.Framework;

namespace MyParcelApi.Tests
{
    [TestFixture]
    public class ShipmentsTests
    {
        private MyParcelApiClient _client;

        [SetUp]
        public void SetUp()
        {
            var apiKey = ConfigurationManager.AppSettings["ApiKey"];
            _client = new MyParcelApiClient(apiKey);
        }

        [Test, Ignore("Deprecated")]
        public void SendOneShipment()
        {
            var conceptShipments = new List<Shipment>
            {
                new Shipment
                {
                    Recipient = new Address
                    {
                        Country = "NL",
                        Person = "Reindert",
                        Company = "Big Sale BV",
                        Street = "Plein 1940-45",
                        Number = "3",
                        NumberSuffix = "b",
                        PostalCode = "2231JE",
                        City = "Rijnsburg",
                        Phone = "123456"
                    },
                    Options = new ShipmentOptions
                    {
                        PackageType = PackageType.Package
                    },
                    Carrier = Carrier.PostNl
                },
                new Shipment
                {
                    Recipient = new Address
                    {
                        Country = "NL",
                        Person = "Piet",
                        Company = "Mega Store",
                        Street = "Koestraat",
                        Number = "55",
                        NumberSuffix = "",
                        PostalCode = "2231JE",
                        City = "Katwijk",
                        Phone = "123-45-235-435",
                    },
                    Options = new ShipmentOptions
                    {
                        PackageType = PackageType.Package,
                        LargeFormat = false,
                        OnlyRecipient = false,
                        Signature = false,
                        Return = false,
                        LabelDescription = "Label description"
                    },
                    Carrier = Carrier.PostNl

                },
                new Shipment
                {
                    Recipient = new Address
                    {
                        Country = "NL",
                        Person = "The insurance man",
                        Company = "Mega Store",
                        Street = "Koestraat",
                        Number = "55",
                        NumberSuffix = "",
                        PostalCode = "2231JE",
                        City = "Katwijk",
                        Phone = "123-45-235-435",
                    },
                    Options = new ShipmentOptions
                    {
                        PackageType = PackageType.Package,
                        LargeFormat = true,
                        OnlyRecipient = true,
                        Signature = true,
                        Return = true,
                        LabelDescription = "1234",
                        Insurance = new Price
                        {
                            Amount = 25000,
                            Currency = "EUR"
                        }
                    },
                    Carrier = Carrier.PostNl
                }
            };
            ProcesShipments(conceptShipments, true);
        }

        [Test, Ignore("Deprecated")]
        public void SendMultipleShipments()
        {
            var conceptShipments = new List<Shipment>
            {
                new Shipment
                {
                    Recipient = new Address
                    {
                        Country = "NL",
                        Person = "Reindert",
                        Company = "Big Sale BV",
                        Street = "Plein 1940-45",
                        Number = "3",
                        NumberSuffix = "b",
                        PostalCode = "2231JE",
                        City = "Rijnsburg"
                    },
                    Options = new ShipmentOptions
                    {
                        PackageType = PackageType.Package
                    },
                    Carrier = Carrier.PostNl
                },
                new Shipment
                {
                    Recipient = new Address
                    {
                        Country = "NL",
                        Person = "Piet",
                        Company = "Mega Store",
                        Street = "Koestraat",
                        Number = "55",
                        NumberSuffix = "",
                        PostalCode = "2231JE",
                        City = "Katwijk",
                        Phone = "123-45-235-435",
                    },
                    Options = new ShipmentOptions
                    {
                        PackageType = PackageType.Package,
                        LargeFormat = false,
                        OnlyRecipient = false,
                        Signature = false,
                        Return = false,
                        LabelDescription = "Label description"
                    },
                    Carrier = Carrier.PostNl

                },
                new Shipment
                {
                    Recipient = new Address
                    {
                        Country = "NL",
                        Person = "The insurance man",
                        Company = "Mega Store",
                        Street = "Koestraat",
                        Number = "55",
                        NumberSuffix = "",
                        PostalCode = "2231JE",
                        City = "Katwijk",
                        Phone = "123-45-235-435",
                    },
                    Options = new ShipmentOptions
                    {
                        PackageType = PackageType.Package,
                        LargeFormat = true,
                        OnlyRecipient = true,
                        Signature = true,
                        Return = true,
                        LabelDescription = "1234",
                        Insurance = new Price
                        {
                            Amount = 25000,
                            Currency = "EUR"
                        }
                    },
                    Carrier = Carrier.PostNl
                }
            };
            ProcesShipments(conceptShipments, true);
        }

        [Test, Ignore("Deprecated")]
        public void SendMailboxShipment()
        {
            var conceptShipments = new List<Shipment>
            {
                new Shipment
                {
                    Recipient = new Address
                    {
                        Country = "NL",
                        Person = "The insurance man",
                        Company = "Mega Store",
                        Street = "Koestraat",
                        Number = "55",
                        NumberSuffix = "",
                        PostalCode = "2231JE",
                        City = "Katwijk",
                        Phone = "123-45-235-435",
                    },
                    Options = new ShipmentOptions
                    {
                        PackageType = PackageType.MailboxPackage,
                        LargeFormat = true,
                        OnlyRecipient = true,
                        Signature = true,
                        Return = true,
                        LabelDescription = "1234",
                        Insurance = new Price
                        {
                            Amount = 25000,
                            Currency = "EUR"
                        }
                    },
                    Carrier = Carrier.PostNl
                }
            };
            ProcesShipments(conceptShipments, true);
        }

        [Test, Ignore("Deprecated")]
        public void SendInternationalShipment()
        {
            var conceptShipments = new List<Shipment>
            {
                new Shipment
                {
                    Recipient = new Address
                    {
                        Country = "NR",
                        Person = "Reindert",
                        Company = "Big Sale BV",
                        Street = "Plein 1940-45",
                        Number = "3",
                        NumberSuffix = "b",
                        PostalCode = "2231JE",
                        City = "Rijnsburg",
                        Phone = "123456"

                    },
                    Options = new ShipmentOptions
                    {
                        PackageType = PackageType.Package
                    },
                    Carrier = Carrier.PostNl
                }
            };
            ProcesShipments(conceptShipments, false);
        }

        [Test]
        public void CreateNationalShipments()
        {
            var conceptShipments = new List<Shipment>
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
                        Person = "Mr. Parcel",
                        Phone = "02012343546",
                        Email = "info@myparcel.nl"

                    },
                    Options = new ShipmentOptions
                    {
                        PackageType = PackageType.Package,
                        OnlyRecipient = false,
                        Signature = false,
                        Return = false,
                    },
                    Carrier = Carrier.PostNl
                }
            };
            ProcesShipments(conceptShipments, false);
        }

        [Test]
        public void CreateInternationalShipment()
        {
            var conceptShipments = new List<Shipment>
            {
                new Shipment
                {
                    Recipient = new Address
                    {
                        Country = "JP",
                        City = "さいたま市",
                        Street = "埼玉県さいたま市浦和区 常盤9－21－21",
                        Person = "Tanaka san",
                        Company = "さいたま国際キリスト教会",
                        Email = "saitamakyokai@gmail.com",
                        Phone = "0081-48-825-6637",

                    },
                    Options = new ShipmentOptions
                    {
                        PackageType = PackageType.Package,
                    },
                    CustomsDeclaration = new CustomsDeclaration
                    {
                        Contents = PackageContents.CommercialGoods,
                        Invoice = "1231235345345",
                        Weight = 30,
                        Items = new []
                        {
                            new CustomsItem
                            {
                                Description = "Sample Product",
                                Amount = 10,
                                Weight = 20,
                                ItemValue = new Price
                                {
                                    Amount = 7000,
                                    Currency = "EUR"
                                },
                                Classification = "0181",
                                Country = "NL"
                            },
                            new CustomsItem
                            {
                                Description = "Sample Product 2",
                                Amount = 5,
                                Weight = 10,
                                ItemValue = new Price
                                {
                                    Amount = 1000,
                                    Currency = "EUR"
                                },
                                Classification = "0181",
                                Country = "BE"
                            }
                        }
                    },
                    PhysicalProperties = new PhysicalProperties
                    {
                        Weight = 30
                    },
                    Carrier = Carrier.PostNl
                }
            };
            ProcesShipments(conceptShipments, false);
        }

        [Test]
        public void CreateShipmentWithPickupLocation()
        {
            var conceptShipments = new List<Shipment>
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
                        DeliveryType = DeliveryType.Pickup,
                        DeliveryDate = DateTime.Today.AddDays(1),
                        OnlyRecipient = false,
                        Signature = true,
                        Return = false
                    },
                    Pickup = new PickupLocation
                    {
                        PostalCode = "2132BH",
                        Street = "Burgemeester van Stamplein",
                        City = "Hoofddorp",
                        Number = "270",
                        LocationName = "Albert Heijn"
                    },
                    Carrier = Carrier.PostNl
                }
            };
            ProcesShipments(conceptShipments, false);
        }

        [Test]
        public void CreateRelatedReturnShipment()
        {
            var conceptReturnShipments = new List<ReturnShipment>
            {
                new ReturnShipment
                {
                    Parent = 21810315,
                    Carrier = Carrier.PostNl,
                    Email = "testing@myparcel.nl",
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
            ProcesReturnShipments(conceptReturnShipments, false);
        }

        [Test]
        public void CreateUnrelatedReturnShipment()
        {
            var conceptReturnShipments = new List<ReturnShipment>
            {
                new ReturnShipment
                {
                    Options = new ShipmentOptions
                    {
                        PackageType = PackageType.Package,
                        OnlyRecipient = false,
                        Signature = true,
                        Return = false,
                        Insurance = new Price
                        {
                            Amount = 0,
                            Currency = "EUR"
                        },
                        LargeFormat = false,
                        LabelDescription = "test"
                    },
                    Carrier = Carrier.PostNl,
                    Email = "testing@myparcel.nl",
                    Name = "Test"
                }
            };
            ProcesUnrelatedReturnShipments(conceptReturnShipments, false);
        }

        [Test]
        public void GenerateUnrelatedReturnShipment()
        {
            var response = _client.GenerateUnrelatedReturnShipment();
            response.Wait();
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(true, response.Result.Link.StartsWith("https://backoffice.myparcel.nl/retour/"));
        }

        [Test]
        public void GetShipmentLabel()
        {
            var response = _client.GetShipmentLabel(new[] { 21811070, 21810315 }, "A4", new[] { 3, 4 });
            response.Wait();
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);
        }

        [Test]
        public void TrackShipment()
        {
            var response = _client.TrackShipment(new[] { 21811070, 21810315 });
            response.Wait();
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);
            Assert.IsTrue(response.Result.Length > 0);
        }

        private void ProcesShipments(List<Shipment> conceptShipments, bool createLabels)
        {
            var response = _client.AddShipment(conceptShipments.ToArray());
            response.Wait();
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Result.Length > 0);

            var shipmentIds = response.Result.Select(obj => obj.Id).ToArray();

            if (createLabels)
            {
                CreateLabels(shipmentIds);
            }
            ValidateShipments(shipmentIds, conceptShipments, createLabels);

            DeleteShipments(shipmentIds);
        }

        private void ProcesReturnShipments(List<ReturnShipment> conceptReturnShipments, bool createLabels)
        {
            var response = _client.AddReturnShipment(conceptReturnShipments.ToArray());
            response.Wait();
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Result.Length > 0);

            var shipmentIds = response.Result.Select(obj => obj.Id).ToArray();

            if (createLabels)
            {
                CreateLabels(shipmentIds);
            }
            ValidateReturnShipments(shipmentIds, conceptReturnShipments, createLabels);

            DeleteShipments(shipmentIds);
        }

        private void ProcesUnrelatedReturnShipments(List<ReturnShipment> conceptReturnShipments, bool createLabels)
        {
            var response = _client.AddUnrelatedReturnShipment(conceptReturnShipments.ToArray());
            response.Wait();
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Result.Length > 0);

            var shipmentIds = response.Result.Select(obj => obj.Id).ToArray();

            if (createLabels)
            {
                CreateLabels(shipmentIds);
            }
            ValidateReturnShipments(shipmentIds, conceptReturnShipments, createLabels);

            DeleteShipments(shipmentIds);
        }

        private void CreateLabels(int[] shipmentIds)
        {
            var response = _client.GetShipmentLabelDownloadLink(shipmentIds);
            response.Wait();
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);
        }

        private void ValidateShipments(int[] shipmentIds, List<Shipment> conceptShipments, bool validateLabel)
        {
            var shipments = _client.GetShipment();
            shipments.Wait();
            Assert.IsNotNull(shipments);
            Assert.IsTrue(shipments.Result.Length > 0);
            foreach (var shipment in shipments.Result)
            {
                var index = shipmentIds.ToList().IndexOf(shipmentIds.FirstOrDefault(obj => obj == shipment.Id));
                if (index >= 0)
                {
                    var conceptShipment = conceptShipments[index];
                    Assert.AreEqual(conceptShipment.Recipient.Country, shipment.Recipient.Country);
                    Assert.AreEqual(conceptShipment.Recipient.Person, shipment.Recipient.Person);
                    Assert.AreEqual(conceptShipment.Recipient.Company, shipment.Recipient.Company);
                    Assert.AreEqual(conceptShipment.Recipient.Street, shipment.Recipient.Street);
                    Assert.AreEqual(conceptShipment.Recipient.StreetAdditionalInfo, shipment.Recipient.StreetAdditionalInfo);
                    Assert.AreEqual(conceptShipment.Recipient.Number, shipment.Recipient.Number);
                    Assert.AreEqual(conceptShipment.Recipient.NumberSuffix, shipment.Recipient.NumberSuffix);
                    Assert.AreEqual(conceptShipment.Recipient.PostalCode, shipment.Recipient.PostalCode);
                    Assert.AreEqual(conceptShipment.Recipient.City, shipment.Recipient.City);
                    Assert.AreEqual(conceptShipment.Recipient.Email, shipment.Recipient.Email);
                    Assert.AreEqual(conceptShipment.Recipient.Phone, shipment.Recipient.Phone);
                    Assert.AreEqual(conceptShipment.Options.PackageType, shipment.Options.PackageType);
                    Assert.AreEqual(conceptShipment.Options.DeliveryType, shipment.Options.DeliveryType);
                    Assert.AreEqual(conceptShipment.Options.DeliveryDate, shipment.Options.DeliveryDate);
                    Assert.AreEqual(conceptShipment.Options.LargeFormat, shipment.Options.LargeFormat);
                    Assert.AreEqual(conceptShipment.Options.OnlyRecipient, shipment.Options.OnlyRecipient);
                    Assert.AreEqual(conceptShipment.Options.Signature, shipment.Options.Signature);
                    Assert.AreEqual(conceptShipment.Options.Return, shipment.Options.Return);
                    Assert.AreEqual(conceptShipment.Options.LabelDescription, shipment.Options.LabelDescription);
                    if (conceptShipment.Options.Insurance != null)
                    {
                        Assert.AreEqual(conceptShipment.Options.Insurance.Amount, shipment.Options.Insurance.Amount);
                        Assert.AreEqual(conceptShipment.Options.Insurance.Currency, shipment.Options.Insurance.Currency);
                    }
                    if (conceptShipment.Pickup != null)
                    {
                        Assert.AreEqual(conceptShipment.Pickup.PostalCode, shipment.Pickup.PostalCode);
                        Assert.AreEqual(conceptShipment.Pickup.Street, shipment.Pickup.Street);
                        Assert.AreEqual(conceptShipment.Pickup.City, shipment.Pickup.City);
                        Assert.AreEqual(conceptShipment.Pickup.Number, shipment.Pickup.Number);
                        Assert.AreEqual(conceptShipment.Pickup.LocationName, shipment.Pickup.LocationName);
                    }
                    if (conceptShipment.CustomsDeclaration != null)
                    {
                        Assert.AreEqual(conceptShipment.CustomsDeclaration.Contents, shipment.CustomsDeclaration.Contents);
                        Assert.AreEqual(conceptShipment.CustomsDeclaration.Invoice, shipment.CustomsDeclaration.Invoice);
                        Assert.AreEqual(conceptShipment.CustomsDeclaration.Weight, shipment.CustomsDeclaration.Weight);
                        for (int i = 0; i < conceptShipment.CustomsDeclaration.Items.Length; i++)
                        {
                            Assert.AreEqual(conceptShipment.CustomsDeclaration.Items[i].Description, shipment.CustomsDeclaration.Items[i].Description);
                            Assert.AreEqual(conceptShipment.CustomsDeclaration.Items[i].Amount, shipment.CustomsDeclaration.Items[i].Amount);
                            Assert.AreEqual(conceptShipment.CustomsDeclaration.Items[i].Weight, shipment.CustomsDeclaration.Items[i].Weight);
                            Assert.AreEqual(conceptShipment.CustomsDeclaration.Items[i].ItemValue.Amount, shipment.CustomsDeclaration.Items[i].ItemValue.Amount);
                            Assert.AreEqual(conceptShipment.CustomsDeclaration.Items[i].ItemValue.Currency, shipment.CustomsDeclaration.Items[i].ItemValue.Currency);
                            Assert.AreEqual(conceptShipment.CustomsDeclaration.Items[i].Classification, shipment.CustomsDeclaration.Items[i].Classification);
                            Assert.AreEqual(conceptShipment.CustomsDeclaration.Items[i].Country, shipment.CustomsDeclaration.Items[i].Country);
                        }
                    }
                    if (conceptShipment.PhysicalProperties != null)
                    {
                        Assert.AreEqual(conceptShipment.PhysicalProperties.Weight, shipment.PhysicalProperties.Weight);
                    }
                    if (validateLabel)
                    {
                        Assert.AreEqual(true, shipment.Barcode.StartsWith("3SMYPA"));
                    }
                }
            }
        }

        private void ValidateReturnShipments(int[] shipmentIds, List<ReturnShipment> conceptReturnShipments, bool validateLabel)
        {
            var shipments = _client.GetShipment();
            shipments.Wait();
            Assert.IsNotNull(shipments);
            Assert.IsTrue(shipments.Result.Length > 0);
            foreach (var shipment in shipments.Result)
            {
                var index = shipmentIds.ToList().IndexOf(shipmentIds.FirstOrDefault(obj => obj == shipment.Id));
                if (index >= 0)
                {
                    var conceptReturnShipment = conceptReturnShipments[index];
                    Assert.AreEqual(conceptReturnShipment.Parent, shipment.ParentId);
                    Assert.AreEqual(conceptReturnShipment.Options.PackageType, shipment.Options.PackageType);
                    Assert.AreEqual(conceptReturnShipment.Options.DeliveryType, shipment.Options.DeliveryType);
                    Assert.AreEqual(conceptReturnShipment.Options.DeliveryDate, shipment.Options.DeliveryDate);
                    Assert.AreEqual(conceptReturnShipment.Options.LargeFormat, shipment.Options.LargeFormat);
                    //Assert.AreEqual(conceptReturnShipment.Options.OnlyRecipient, shipment.Options.OnlyRecipient);
                    //Assert.AreEqual(conceptReturnShipment.Options.Signature, shipment.Options.Signature);
                    Assert.AreEqual(conceptReturnShipment.Options.Return, shipment.Options.Return);
                    //Assert.AreEqual(conceptReturnShipment.Options.LabelDescription, shipment.Options.LabelDescription);
                    if (conceptReturnShipment.Options.Insurance != null)
                    {
                        //Assert.AreEqual(conceptReturnShipment.Options.Insurance.Amount, shipment.Options.Insurance.Amount);
                        Assert.AreEqual(conceptReturnShipment.Options.Insurance.Currency, shipment.Options.Insurance.Currency);
                    }
                    if (validateLabel)
                    {
                        Assert.AreEqual(true, shipment.Barcode.StartsWith("3SMYPA"));
                    }
                }
            }
        }

        private void DeleteShipments(int[] shipmentIds)
        {
            var result = _client.DeleteShipment(shipmentIds);
            result.Wait();
        }
    }
}
