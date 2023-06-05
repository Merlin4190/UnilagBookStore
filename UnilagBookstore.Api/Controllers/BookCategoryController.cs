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
    public class BookCategoryController : ControllerBase
    {
        private readonly IBookCategoryService _catService;
        private readonly ILoggerService _logger;

        public BookCategoryController(IBookCategoryService catService, ILoggerService logger)
        {
            _logger = logger;
            _catService = catService;
        }

        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponseDto), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseDto), (int)HttpStatusCode.OK)]
        [HttpPost]
        public async Task<IActionResult> CreateBookCategoryAsync(CreateBookCategoryDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _catService.CreateAsync(model));
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
        public async Task<IActionResult> UpdateBookCategoryAsync(GetBookCategoryDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _catService.UpdateAsync(model));
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
        [ProducesResponseType(typeof(ServiceResponseDto<IEnumerable<GetBookCategoryDto>>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseDto<IEnumerable<GetBookCategoryDto>>), (int)HttpStatusCode.OK)]
        [HttpGet]
        public async Task<IActionResult> GetBookCategoriesAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _catService.GetAllAsync());
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
        [ProducesResponseType(typeof(ServiceResponseDto<GetBookCategoryDto>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseDto<GetBookCategoryDto>), (int)HttpStatusCode.OK)]
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetBookCategoryAsync([FromRoute] BookCategoryDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _catService.GetAsync(model));
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
        public async Task<IActionResult> DeleteBookCategoryAsync(BookCategoryDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _catService.DeleteAsync(model));
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