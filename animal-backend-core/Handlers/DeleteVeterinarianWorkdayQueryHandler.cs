using animal_backend_core.Queries;
using animal_backend_infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace animal_backend_core.Handlers;

public class DeleteVeterinarianWorkdayQueryHandler(AnimalDbContext dbContext)
	: IRequestHandler<DeleteVeterinarianWorkdayQuery, bool>
{
	public async Task<bool> Handle(DeleteVeterinarianWorkdayQuery query, CancellationToken cancellationToken)
	{
		var workHoursToDelete = await dbContext.Users
			.Where(u => u.VeterinarianId == query.VeterinarianId)
			.SelectMany(u => u.Veterinarian!.WorkHours)
			.Where(wh => wh.Date == query.Date)
			.ToListAsync(cancellationToken);

		if (workHoursToDelete.Any(wh => wh.Taken))
		{
			return false;
		}
		
		dbContext.RemoveRange(workHoursToDelete);
		await dbContext.SaveChangesAsync(cancellationToken);

		return true;


	}
}