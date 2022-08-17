using Contracts;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public  class PositionController: ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public PositionController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager ?? throw new ArgumentNullException(nameof(serviceManager));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPositions()
        {
            try
            {
                var positions = await _serviceManager.PositionService.GetAllAsync();

                return Ok(positions);
            }
            catch (Exception)
            {
                return BadRequest("Internal server error");
            }
        }

        [HttpGet("id")]
        public async Task<IActionResult> GePositionById(Guid id)
        {
            try
            {
                var employeeDto = await _serviceManager.PositionService.GetByIdAsync(id);

                return Ok(employeeDto);
            }
            catch (Exception ex)
            {

                return BadRequest("Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePosition([FromBody] PositionForPersitenceDto positionForCreation)
        {
            try
            {
                if (positionForCreation is null)
                {
                    return BadRequest("employeeForCreation object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var positionDTO =  await _serviceManager.PositionService.CreateAsync(positionForCreation);

                return Ok(positionDTO);
            }
            catch (Exception)
            {
                return BadRequest("Internal server error");
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdatePosition(Guid id, [FromBody] PositionForPersitenceDto positionForUpdate)
        {
            try
            {
                if (positionForUpdate is null)
                {
                    return BadRequest("employeeForCreation object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                await _serviceManager.PositionService.UpdateAsync(id, positionForUpdate);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Internal server error");
            }

        }
    }
}
