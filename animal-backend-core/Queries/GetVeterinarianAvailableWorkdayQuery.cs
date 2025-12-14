using animal_backend_domain.Dtos.Workday;
using MediatR;

namespace animal_backend_core.Queries;

public class GetVeterinarianAvailableWorkdayQuery : IRequest<WorkdayDto>
{
	public required Guid VeterinarianId { get; set; }
}