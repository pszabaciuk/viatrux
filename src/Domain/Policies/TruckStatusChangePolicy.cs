using Domain.Trucks;

namespace Domain.Policies;

public sealed class TruckStatusChangePolicy : IPolicy
{
    public bool IsValidPolicy(TruckStatus currentTruckStatus, TruckStatus newTruckStatus)
    {
        if (currentTruckStatus == TruckStatus.OutOfService)
        {
            return true;
        }

        switch (newTruckStatus)
        {
            case TruckStatus.OutOfService:
                return true;
            case TruckStatus.Loading:
                return currentTruckStatus == TruckStatus.Returning;
            case TruckStatus.ToJob:
                return currentTruckStatus == TruckStatus.Loading;
            case TruckStatus.AtJob:
                return currentTruckStatus == TruckStatus.ToJob;
            case TruckStatus.Returning:
                return currentTruckStatus == TruckStatus.AtJob;
            default:
                return false;
        }
    }
}