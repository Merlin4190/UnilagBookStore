using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using UnilagBookstore.Domain.Const;
using UnilagBookstore.Domain.DTOs;
using UnilagBookstore.Services.Exceptions;
using UnilagBookstore.Services.Interfaces;

namespace UnilagBookstore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly ILoggerService _logger;

        public UserController(IUserService userService, IAuthService authService, ILoggerService logger)
        {
            _userService = userService;
            _authService = authService;
            _logger = logger;
        }

        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponseDto), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseDto), (int)HttpStatusCode.OK)]
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(CreateUserDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _userService.RegisterUserAsync(model));
                }
                return BadRequest(ModelState);
            }
            catch (AppException ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, new ServiceResponseDto()
                {
                    StatusCode = ResponseCode.Error,
                    StatusMessage = ex.Message,
                });
            }
            
        }

        [AllowAnonymous]
        [ProducesResponseType(typeof(ServiceResponseDto<LoginResponseDto>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseDto<LoginResponseDto>), (int)HttpStatusCode.OK)]
        [Produces("application/json")]
        [HttpPost, Route("login")]
        public async Task<IActionResult> UserLoginCommand([FromBody] LoginUserDto model)
        {
            _logger.LogInfo($"UserLoginCommand - Details");
            try
            {

                if (model == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var response = await _authService.LoginAsync(model);
                if (response.StatusCode != "00") return new BadRequestObjectResult(response);
                _logger.LogInfo($"User login successful, response ");
                return Ok(response);
            }
            catch (AppException ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, new ServiceResponseDto()
                {
                    StatusCode = ResponseCode.Error,
                    StatusMessage = ex.Message,
                });
            }


        }
    }
}