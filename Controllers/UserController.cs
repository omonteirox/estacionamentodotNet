

using estacionamento.DTOS;
using estacionamento.Models;
using estacionamento.Services;
using estacionamento.Utils;
using Microsoft.AspNetCore.Mvc;

namespace estacionamento.Controllers
{
    [Controller]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;
        public UserController(UserService userService)
        {
            _service = userService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                var hash = TokenHelper.GenerateToken(result.UserName, result.TypeUserEnum);
                var dto = new UserDTO(result.Id,result.UserName, result.PasswordHash, hash , DateTime.Now.AddHours(1).ToUniversalTime());
                return Ok(new ResponseModel<UserDTO>(dto));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<Exception>(ex.Message));
            }

        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] User user)
        {
            try
            {
                var result = await _service.CreateAsync(user);
                return Ok(new ResponseModel<User>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<Exception>(ex.Message));
            }

        }
        [HttpPost("/login")]
        public async Task<ActionResult> LoginAsync([FromBody] User user)
        {
            try
            {
                var result = await _service.Authenticate(user.UserName, user.PasswordHash);
                var newToken = TokenHelper.GenerateToken(result.UserName, result.TypeUserEnum);
                var dto = new UserDTO(result.Id, result.UserName, result.PasswordHash, newToken, DateTime.Now.AddHours(1).ToUniversalTime());
                return Ok(new ResponseModel<UserDTO>(dto));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<Exception>(ex.Message));
            }

        }


    }
}
