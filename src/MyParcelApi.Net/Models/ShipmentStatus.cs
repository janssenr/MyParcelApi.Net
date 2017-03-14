namespace MyParcelApi.Net.Models
{
    public enum ShipmentStatus
    {
        PendingConcept = 1,
        PendingRegistered = 2,
        EnrouteHandedToCarrier = 3,
        EnrouteSorting = 4,
        EnrouteDistribution = 5,
        EnrouteCustoms = 6,
        DeliveredAtRecipient = 7,
        DeliveredReadyForPickup = 8,
        DeliveredPackagePickedUp = 9,
        DeliveredReturnShipmentReadyForPickup = 10,
        DeliveredReturnShipmentPackagePickedUp = 11,
        PrintedLetter = 12,
        Credit = 13,
        InactiveConcept = 30,
        InactiveRegistered = 31,
        InactiveEnrouteHandedToCarrier = 32,
        InactiveEnrouteSorting = 33,
        InactiveEnrouteDistribution = 34,
        InactiveEnrouteCustoms = 35,
        InactiveDeliveredAtRecipient = 36,
        InactiveDeliveredReadyForPickup = 37,
        InactiveDeliveredPackagePickedUp = 38,
        InactiveUnknown = 99,
        CreditRejected = 100,
        CreditApproved = 101
    }
}
