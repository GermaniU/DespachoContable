using Contracts;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeFilterController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public EmployeeFilterController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager ?? throw new ArgumentNullException(nameof(serviceManager));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployeesFiltered([FromQuery] EmployeeFiltersDTO employeeFilters)
        {
            try
            {
                var employees = await _serviceManager.EmployeeServices.GetAllAsyncFiltered(employeeFilters);

                return Ok(employees);

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
