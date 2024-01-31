using Domain.Trucks;
using MediatR;
using Persistence;

namespace Application.Trucks;

public sealed class GetAll
{
    public sealed class Query : IRequest<Result<IReadOnlyCollection<TruckDto>>> { }

    public sealed class Handler : IRequestHandler<Query, Result<IReadOnlyCollection<TruckDto>>>
    {
        public Handler()
        {
        }

        public async Task<Result<IReadOnlyCollection<TruckDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            List<Truck> trucks = Data.Trucks;

            IEnumerable<TruckDto> result = trucks.Select(s => new TruckDto(s.Id, s.TruckCode, s.Name, s.TruckStatus.ToString(), s.Description));

            return Result<IReadOnlyCollection<TruckDto>>.Success(result.ToList());
        }
    }
}