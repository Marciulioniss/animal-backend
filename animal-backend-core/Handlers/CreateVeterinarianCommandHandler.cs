using System;
using animal_backend_infrastructure;
using MediatR;
using animal_backend_core.Commands;

namespace animal_backend_core.Handlers;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}

public class CreateVeterinarianCommandHandler(AnimalDbContext dbContext)
    : IRequestHandler<CreateVeterinarianCommand, Guid>
{
    public async Task<Guid> Handle(CreateVeterinarianCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.FindAsync(request.UserId);
        if (user == null)
        {
            throw new NotFoundException($"User with ID {request.UserId} not found");
        }

        var veterinarian = new animal_backend_domain.Entities.Veterinarian
        {
            Id = Guid.NewGuid(),
            // Copy from existing user
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            Password = user.Password,
            Role = animal_backend_domain.Types.RoleType.Veterinarian,
            PhoneNumber = user.PhoneNumber,
            PhotoUrl = user.PhotoUrl,
            // Vet-specific data from request
            BirthDate = request.BirthDate,
            Rank = request.Rank,
            Responsibilities = request.Responsibilities,
            Education = request.Education,
            Salary = request.Salary,
            FullTime = request.FullTime,
            HireDate = request.HireDate,
            ExperienceYears = request.ExperienceYears,
            Gender = request.Gender
        };

        dbContext.Veterinarians.Add(veterinarian);
        user.VeterinarianId = veterinarian.Id;
        dbContext.Users.Update(user);

        await dbContext.SaveChangesAsync(cancellationToken);
        return veterinarian.Id;
    }
}