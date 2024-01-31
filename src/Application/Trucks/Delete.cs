using Domain.Trucks;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Trucks;


public sealed class Delete
{
    public class Command : IRequest<Result<Unit>>
    {
        public string Id { get; set; } = default!;
    }

    public sealed class Handler : IRequestHandler<Command, Result<Unit>>
    {
        public Handler()
        {
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken ct)
        {
            List<Truck> trucks = Data.Trucks;

            Truck? truck = trucks.Find(d => d.Id == Guid.Parse(request.Id));
            if (truck != null)
            {
                trucks.Remove(truck);
            }

            bool success = true; // only for mocking purposes

            if (success)
            {
                return Result<Unit>.Success(Unit.Value);
            }

            return Result<Unit>.Failure("Deleting truck was failed"); // add error details
        }
    }
}