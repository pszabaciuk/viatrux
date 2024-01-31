using Application.Extensions;
using Domain.Trucks;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Trucks;

public sealed class Edit
{
    public class Command : IRequest<Result<Unit>>
    {
        public string Id { get; set; } = default!;
        public string TruckCode { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string TruckStatus { get; set; } = default!;
    }

    public sealed class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.TruckCode).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.TruckStatus).NotEmpty();
        }
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
            if (truck == null)
            {
                return Result<Unit>.Failure($"There is no truck with id {request.Id}");
            }

            if (!Validator.IsTruckCodeUniqueness(request.TruckCode))
            {
                return Result<Unit>.Failure("Truck code must by unique.");
            }

            TruckStatus newTruckStatus;
            if (!Enum.TryParse(request.TruckStatus, out newTruckStatus))
            {
                return Result<Unit>.Failure($"Truck status is invalid. Valid values: {TruckStatus.OutOfService.ToCommaSeparatedString()}");
            }

            if (!truck.TrySetStatus(newTruckStatus))
            {
                return Result<Unit>.Failure($"Change status from `{truck.TruckStatus}` to `{newTruckStatus}` is not possible.");
            }

            truck.TruckCode = request.TruckCode;
            truck.Name = request.Name;
            truck.Description = request.Description?.NullIfEmpty();

            bool success = true; // only for mocking purposes

            if (success)
            {
                return Result<Unit>.Success(Unit.Value);
            }

            return Result<Unit>.Failure("Editing truck was failed"); // add error details
        }
    }
}