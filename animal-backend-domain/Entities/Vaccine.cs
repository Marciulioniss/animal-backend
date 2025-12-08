namespace animal_backend_domain.Entities;

public class Vaccine : Entity
{   
    public required string Name { get; set; }
    public DateTime Date { get; set; }
    public required string Description { get; set; }
    public required string Manufacturer { get; set; }
}