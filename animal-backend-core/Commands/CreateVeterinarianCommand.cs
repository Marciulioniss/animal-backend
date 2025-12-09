using MediatR;
using animal_backend_domain.Types;
namespace animal_backend_core.Commands
{
    public record CreateVeterinarianCommand(
        string Name,
        string Surname,
        string Email,
        string Password,
        RoleType Role,
        string PhoneNumber,
        string PhotoUrl,
        DateTime BirthDate,
        string Rank,
        string Responsibilities,
        string Education,
        double Salary,
        double FullTime,
        DateTime HireDate,
        int ExperienceYears,
        GenderType Gender
    ) : IRequest<Guid>;
}