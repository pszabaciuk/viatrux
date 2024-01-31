using Persistence;

namespace Application.Trucks;

public static class Validator
{
    public static bool IsTruckCodeUniqueness(string truckCode)
    {
        return !Data.Trucks.Any(a => a.TruckCode == truckCode);
    }
}