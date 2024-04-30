using estacionamento.DTOS;
using estacionamento.Models;
using estacionamento.Services;
using Microsoft.AspNetCore.Mvc;

namespace estacionamento.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class InOutEstablishmentController : ControllerBase
    {
        private readonly InOutEstablishmentService _inOutService;

        public InOutEstablishmentController(InOutEstablishmentService service)
        {
            _inOutService = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            try
            {
                var result = await _inOutService.GetAllAsync();
                return Ok(new ResponseModel<List<InOutEstablishment>>(result));
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
                var result = await _inOutService.GetByIdAsync(id);
                return Ok(new ResponseModel<InOutEstablishment>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<Exception>(ex.Message));
            }

        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] CreateinOutEstablishmentDTO dto)
        {
            var InOutEstablishment = new InOutEstablishment();
            InOutEstablishment.EstablishmentId= dto.EstablishmentId;
            InOutEstablishment.VehicleId = dto.VehicleId;

            try
            {
                var result = await _inOutService.CreateAsync(InOutEstablishment);
                return Ok(new ResponseModel<InOutEstablishment>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<Exception>(ex.Message));
            }

        }

        [HttpPost("CloseInOutAsync")]
        public async Task<ActionResult> CloseInOutEstablishmentAsync([FromBody] DateEndinOutEstablishmentDTO dto)
        {
 
            try
            {
                var InOutEstablishment = await _inOutService.GetByIdAsync(dto.Id);
                var result = await _inOutService.closeEstablishmentAsync(InOutEstablishment);
                return Ok(new ResponseModel<InOutEstablishment>(result));

            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<Exception>("Erro interno, contate nossa equipe de suporte."));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                await _inOutService.RemoveAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<Exception>(ex.Message));
            }

        }
    }
}
