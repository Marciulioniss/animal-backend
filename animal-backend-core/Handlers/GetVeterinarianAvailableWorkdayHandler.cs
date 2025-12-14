using animal_backend_core.Queries;
using animal_backend_domain.Dtos.Workday;
using animal_backend_infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace animal_backend_core.Handlers;

public class GetVeterinarianAvailableWorkdayHandler(AnimalDbContext dbContext)
	: IRequestHandler<GetVeterinarianAvailableWorkdayQuery, WorkdayDto>
{
	public async Task<WorkdayDto> Handle(GetVeterinarianAvailableWorkdayQuery query, CancellationToken cancellationToken)
	{
		var workHours = await dbContext.Users
			.Where(u => u.VeterinarianId == query.VeterinarianId)
			.SelectMany(u => u.Veterinarian!.WorkHours)
			.Where(wh => wh.Taken == false)
			.Select(wh => new { wh.Date, wh.Hour })
			.ToListAsync(cancellationToken);

		var workdayDto = new WorkdayDto
		{
			WorkHours = workHours
				.GroupBy(wh => wh.Date)
				.ToDictionary(
					g => g.Key,
					g => g.Select(wh => wh.Hour).ToList()
				)
		};

		return workdayDto;
	}
}