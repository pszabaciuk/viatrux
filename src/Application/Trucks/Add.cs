using Application.Extensions;
using Domain.Trucks;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Trucks;

public sealed class Add
{
    public class Command : IRequest<Result<Guid>>
    {
        public string TruckCode { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }

    public sealed class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.TruckCode).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }

    public sealed class Handler : IRequestHandler<Command, Result<Guid>>
    {
        public Handler()
        {
        }

        public async Task<Result<Guid>> Handle(Command request, CancellationToken ct)
        {
            if (!Validator.IsTruckCodeUniqueness(request.TruckCode))
            {
                return Result<Guid>.Failure("Truck code must by unique.");
            }

            Truck truck = Truck.Create(request.TruckCode, request.Name, request.Description?.NullIfEmpty());
            Data.Trucks.Add(truck);

            bool success = true; // only for mocking purposes

            if (success)
            {
                return Result<Guid>.Success(truck.Id);
            }

            return Result<Guid>.Failure("Creating new truck was failed"); // add error details
        }
    }
}