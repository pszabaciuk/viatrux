namespace Application.Trucks;

public record TruckDto(Guid Id, string TruckCode, string Name, string truckStatus, string? Description = null);