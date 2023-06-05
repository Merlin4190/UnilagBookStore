using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UnilagBookstore.Domain.Const;
using UnilagBookstore.Domain.DTOs;
using UnilagBookstore.Services.Exceptions;
using UnilagBookstore.Services.Interfaces;

namespace UnilagBookstore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILoggerService _logger;

        public BookController(IBookService bookService, ILoggerService logger)
        {
            _logger = logger;
            _bookService = bookService;
        }

        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponseDto), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseDto), (int)HttpStatusCode.OK)]
        [HttpPost]
        public async Task<IActionResult> CreateBookAsync(CreateBookDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _bookService.CreateAsync(model));
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

        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponseDto), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseDto), (int)HttpStatusCode.OK)]
        [HttpPost("update")]
        public async Task<IActionResult> UpdateBookAsync(GetBookDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _bookService.UpdateAsync(model));
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

        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponseDto<IEnumerable<GetBookDto>>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseDto<IEnumerable<GetBookDto>>), (int)HttpStatusCode.OK)]
        [HttpGet]
        public async Task<IActionResult> GetBooksAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _bookService.GetAllAsync());
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

        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponseDto<GetBookDto>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseDto<GetBookDto>), (int)HttpStatusCode.OK)]
        [HttpGet("id")]
        public async Task<IActionResult> GetBookAsync([FromRoute] BookDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _bookService.GetAsync(model));
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

        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponseDto), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseDto), (int)HttpStatusCode.OK)]
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteBookAsync(BookDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _bookService.DeleteAsync(model));
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
    }
}