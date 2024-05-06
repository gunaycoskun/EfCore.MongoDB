namespace EFCoreMongo.API.Models;

public sealed record CreateVehicleDto
{
    public string Name { get; set; }
    public string Model { get; set; }
    public string Manufacturer { get; set; }
}