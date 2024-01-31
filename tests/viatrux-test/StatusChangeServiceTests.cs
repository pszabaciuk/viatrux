

using Domain.Policies;
using Domain.Trucks;

namespace viatrux_test;

public sealed class StatusChangeServiceTests
{
    private TruckStatusChangePolicy _truckStatusChangePolicy = new TruckStatusChangePolicy();

    [Theory]
    [InlineData(TruckStatus.ToJob, TruckStatus.OutOfService, true)]
    [InlineData(TruckStatus.AtJob, TruckStatus.OutOfService, true)]
    [InlineData(TruckStatus.Returning, TruckStatus.OutOfService, true)]
    [InlineData(TruckStatus.Loading, TruckStatus.OutOfService, true)]
    [InlineData(TruckStatus.OutOfService, TruckStatus.ToJob, true)]
    [InlineData(TruckStatus.OutOfService, TruckStatus.AtJob, true)]
    [InlineData(TruckStatus.OutOfService, TruckStatus.Returning, true)]
    [InlineData(TruckStatus.OutOfService, TruckStatus.Loading, true)]
    [InlineData(TruckStatus.Loading, TruckStatus.ToJob, true)]
    [InlineData(TruckStatus.ToJob, TruckStatus.AtJob, true)]
    [InlineData(TruckStatus.AtJob, TruckStatus.Returning, true)]
    [InlineData(TruckStatus.Returning, TruckStatus.Loading, true)]
    [InlineData(TruckStatus.ToJob, TruckStatus.Loading, false)]
    public void Verify_IsValidStatusTransition(TruckStatus currentTruckStatus, TruckStatus newTruckStatus, bool expected)
    {
        bool result = _truckStatusChangePolicy.IsValidPolicy(currentTruckStatus, newTruckStatus);

        Assert.Equal(result, expected);
    }
}