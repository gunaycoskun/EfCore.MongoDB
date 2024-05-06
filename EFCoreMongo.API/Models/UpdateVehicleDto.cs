namespace EFCoreMongo.API.Models;

public sealed record UpdateVehicleDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Model { get; set; }
    public string Manufacturer { get; set; }
}