using estacionamento.DTOS;
using estacionamento.Models;
using estacionamento.Services;
using Microsoft.AspNetCore.Mvc;

namespace estacionamento.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class EstablishmentController : ControllerBase
    {
        private readonly EstablishmentService _service;
        public EstablishmentController(EstablishmentService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            try
            {
                var result = await _service.GetAllAsync();
                return Ok(new ResponseModel<List<Establishment>>(result));
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
                var result = await _service.GetByIdAsync(id);
                return Ok(new ResponseModel<Establishment>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<Exception>(ex.Message));
            }

        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] EstablishmentDTO dto)
        {
            var establishment = new Establishment();
            establishment.Phone = dto.Phone;
            establishment.Address  = dto.Address;
            establishment.Name = dto.Name;
            establishment.MotorcycleSpotsAvailable = dto.MotorCycleSpots;
            establishment.CarSpotsAvailable = dto.carSpots;
            try
            {
                var result = await _service.CreateAsync(establishment);
                return Ok(new ResponseModel<Establishment>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<Exception>(ex.Message));
            }

        }
        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] Establishment establishment)
        {
            try
            {
                var result = await _service.UpdateAsync(establishment);
                return Ok(new ResponseModel<Establishment>(result));
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
                await _service.RemoveAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<Exception>(ex.Message));
            }

        }
    }
}
