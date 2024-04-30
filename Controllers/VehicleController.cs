using estacionamento.DTOS;
using estacionamento.Models;
using estacionamento.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace estacionamento.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly VehicleService _vehicleService;

        public VehicleController(VehicleService service)
        {
            _vehicleService = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            try
            {
                var result = await _vehicleService.GetAllAsync();
                return Ok(new ResponseModel<List<Vehicle>>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<Exception>(ex.Message));
            }
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            try
            {
                var result = await _vehicleService.GetByIdAsync(id);
                return Ok(new ResponseModel<Vehicle>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<Exception>(ex.Message));
            }
            
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] VehicleDTO dto)
        {
            var vehicle = new Vehicle();
            vehicle.Brand = dto.Brand;
            vehicle.Color = dto.Color;
            vehicle.Plate = dto.Plate;
            vehicle.Type = dto.Type;
            vehicle.EstablishmentId = dto.EstablishmentId;

            try
            {
                var result = await _vehicleService.CreateAsync(vehicle);
                return Ok(new ResponseModel<Vehicle>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<Exception>(ex.Message));
            }
            
        }

        [HttpGet("getByPlate/{plate}")]
        public async Task<ActionResult> GetByPlateAsync(string plate)
        {
            try
            {
                var result = await _vehicleService.GetByPlateAsync(plate);
                return Ok(new ResponseModel<Vehicle>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<Exception>(ex.Message));
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                await _vehicleService.RemoveAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<Exception>(ex.Message));
            }
            
        }
    }
}
