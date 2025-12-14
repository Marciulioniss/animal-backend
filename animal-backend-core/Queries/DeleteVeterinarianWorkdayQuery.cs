using MediatR;

namespace animal_backend_core.Queries;

public class DeleteVeterinarianWorkdayQuery : IRequest<bool>
{
	public required Guid VeterinarianId { get; set; }
	public required DateOnly Date { get; set; }
}