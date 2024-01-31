using Domain.Trucks;
using MediatR;
using Persistence;

namespace Application.Trucks;

public sealed class Get
{
    public sealed class Query : IRequest<Result<TruckDto>>
    {
        public string Id { get; set; } = default!;
    }

    public sealed class Handler : IRequestHandler<Query, Result<TruckDto>>
    {
        public Handler()
        {
        }

        public async Task<Result<TruckDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            List<Truck> trucks = Data.Trucks;

            Truck? truck = trucks.Find(d => d.Id == Guid.Parse(request.Id));
            if (truck == null)
            {
                return Result<TruckDto>.Failure($"There is no truck with id {request.Id}");
            }

            TruckDto result = new TruckDto(truck.Id, truck.TruckCode, truck.Name, truck.TruckStatus.ToString(), truck.Description);

            return Result<TruckDto>.Success(result);
        }
    }
}