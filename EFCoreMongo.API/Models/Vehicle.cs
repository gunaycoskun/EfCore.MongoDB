using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace EFCoreMongo.API.Models;

[Collection("Vehicles")]
public class Vehicle
{
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public string Model { get; set; }
    public string Manufacturer { get; set; }
}