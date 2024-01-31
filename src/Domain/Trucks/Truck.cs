using Domain.Policies;

namespace Domain.Trucks;

public sealed class Truck
{
    private static readonly TruckStatusChangePolicy _truckStatusChangePolicy = new TruckStatusChangePolicy();
    private TruckStatus _truckStatus = TruckStatus.OutOfService;

    public Truck(Guid id, string truckCode, string name, string? description = null)
    {
        Id = id;
        TruckCode = truckCode;
        Name = name;
        Description = description;
        SetOutOfService();
    }

    public static Truck Create(string truckCode, string name, string? description = null)
    {
        return new Truck(Guid.NewGuid(), truckCode, name, description);
    }

    public Guid Id { get; private set; }

    public string TruckCode { get; set; }

    public string Name { get; set; }

    public TruckStatus TruckStatus
    {
        get
        {
            return _truckStatus;
        }
    }

    public string? Description { get; set; } = null;

    public void SetOutOfService()
    {
        _truckStatus = TruckStatus.OutOfService;
    }

    public bool TrySetStatus(TruckStatus truckStatus)
    {
        if (_truckStatusChangePolicy.IsValidPolicy(_truckStatus, truckStatus))
        {
            _truckStatus = truckStatus;
            return true;
        }

        return false;
    }
}