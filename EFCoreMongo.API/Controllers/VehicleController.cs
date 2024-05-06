using EFCoreMongo.API.Data;
using EFCoreMongo.API.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace EFCoreMongo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly VehicleDbContext _context;

        public VehicleController(VehicleDbContext context)
        {
            _context = context;
        }

      
        [HttpGet]
        public async Task<IEnumerable<VehicleDto>> GetVehicles()
        {
            return await _context.Vehicles
                .Select(v => new VehicleDto
                {
                    Id = v.Id.ToString(),
                    Name = v.Name,
                    Model = v.Model,
                    Manufacturer = v.Manufacturer
                }).ToListAsync();
        }

   
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(string id)
        {
            var vehicle = await _context.Vehicles.FindAsync(ObjectId.Parse(id));

            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

    
        [HttpPut]
        public async Task<IActionResult> PutVehicle(UpdateVehicleDto vehicle)
        {
            var vehicleToUpdate = await _context.Vehicles.FindAsync(ObjectId.Parse(vehicle.Id));
            
            if (vehicleToUpdate == null)
            {
                return NotFound();
            }
            
            vehicleToUpdate.Name = vehicle.Name;
            vehicleToUpdate.Model = vehicle.Model;
            vehicleToUpdate.Manufacturer = vehicle.Manufacturer;
            
            _context.Entry(vehicleToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(vehicle.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

       
        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle(CreateVehicleDto dto)
        {
            var vehicle = new Vehicle
            {
                Name = dto.Name,
                Model = dto.Model,
                Manufacturer = dto.Manufacturer
            };
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicle", new { id = vehicle.Id.ToString() }, vehicle);
        }

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(string id)
        {
            var vehicle = await _context.Vehicles.FindAsync(ObjectId.Parse(id));
            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehicleExists(string id)
        {
            return _context.Vehicles.Any(e => e.Id.ToString() == id);
        }
    }
}