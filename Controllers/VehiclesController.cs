using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleRegistrationSystemWebAPI.Data;
using VehicleRegistrationSystemWebAPI.Models;

namespace VehicleRegistrationSystemWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        #region Dependencies Injection
        
        private readonly AppDbContext _context;
        public VehiclesController(AppDbContext context)
        {
            _context = context;
        }
        #endregion


        #region Read all vehicles list
        [HttpGet] // GET: api/Vehicles
        public async Task<IActionResult> GetVehicles()
        {
            var vehicles = await _context.Vehicles.Include(v => v.Registration).ToListAsync();
            return Ok(vehicles);
        }
        #endregion


        #region Read a vehicle by id        
        [HttpGet("{id:guid}")] // GET: api/Vehicle/3
        public async Task<IActionResult> VehicleDetails(Guid id)
        {
            var vehicle = await FindVehicleWithRegistration(id);

            if (vehicle == null)
                return NotFound();

            return Ok(vehicle);
        }
        #endregion


        #region Create a new vehicle
        [HttpPost] // POST: api/Vehicle
        public async Task<IActionResult> CreateVehicle(Vehicle vehicle)
        {
            vehicle.Id = Guid.NewGuid();
            vehicle.Registration.VehicleId = vehicle.Id;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
            return Ok(vehicle);
        }
        #endregion


        #region Update an existing vehicle
        [HttpPut("{id:guid}")] // PUT: api/Vehicle/3
        public async Task<IActionResult> UpdateVehicle(Guid id, Vehicle vehicle)
        {
            if (id != vehicle.Id)
                return BadRequest("Vehicle id mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var duplicatePlate = await _context.Vehicles.Include(v => v.Registration)
                .Where(v => v.Id != id &&
                    v.Registration.PlateNumber == vehicle.Registration.PlateNumber).CountAsync();

            if (vehicle.Registration.PlateNumber != "N/D" && duplicatePlate > 0)
                return BadRequest("Another vehicle with this registration plate already exists.");

            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
            return Ok(vehicle);
        }
        #endregion


        #region Delete a vehicle
        [HttpDelete("{id:guid}")] // DELETE: api/Vehicle/3
        public async Task<IActionResult> DeleteVehicle(Guid id)
        {
            var vehicle = await FindVehicleWithRegistration(id);

            if (vehicle == null)
                return NotFound();

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
            return Ok();
        }
        #endregion


        #region Helper Methods
        public async Task<Vehicle> FindVehicleWithRegistration(Guid id)
        {
            return await _context.Vehicles.Include(v => v.Registration)
                                                 .FirstOrDefaultAsync(v => v.Id == id);
        }
        #endregion
    }
}
