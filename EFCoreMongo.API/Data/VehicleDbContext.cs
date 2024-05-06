using EFCoreMongo.API.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace EFCoreMongo.API.Data;

public class VehicleDbContext:DbContext
{
    public VehicleDbContext(DbContextOptions<VehicleDbContext> options):base(options)
    {
        
    }
    public DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Vehicle>().ToCollection("vehicles");
    }
}