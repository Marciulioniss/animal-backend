namespace animal_backend_domain.Dtos.Workday;

public class WorkdayDto
{
	public Dictionary<DateOnly, List<int>> WorkHours { get; set; } = new();
}