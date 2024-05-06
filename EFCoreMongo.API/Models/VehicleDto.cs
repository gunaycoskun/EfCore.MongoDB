namespace EFCoreMongo.API.Models;

public sealed record VehicleDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Model { get; set; }
    public string Manufacturer { get; set; }
}