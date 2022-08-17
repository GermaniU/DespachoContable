using Contracts;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public EmployeeController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager ?? throw new ArgumentNullException(nameof(serviceManager));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var employees = await _serviceManager.EmployeeServices.GetAllAsync();

                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest("Internal server error");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeesById(Guid id)
        {
            try
            {
                var employeeDto = await _serviceManager.EmployeeServices.GetByIdAsync(id);

                return Ok(employeeDto);

            }
            catch (Exception)
            {
                return BadRequest("Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeForCreationDTO employeeForCreation)
        {
            try
            {
                if (employeeForCreation is null)
                {
                    return BadRequest("employeeForCreation object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var employeeDto = await _serviceManager.EmployeeServices.CreateAsync(employeeForCreation);

                return Ok(employeeDto);
            }
            catch (Exception ex)
            {
                return BadRequest("Internal server error");
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UnsubscribeEmployee(Guid id)
        {
            try
            {
                var employeeDTO = await _serviceManager.EmployeeServices.UnsubscribeAsync(id);

                return Ok(employeeDTO);
            }
            catch (Exception)
            {
                return BadRequest("Internal server error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] EmployeeForUpdateDTO employeeForUpdate)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(employeeForUpdate.Telefono)
                    && string.IsNullOrWhiteSpace(employeeForUpdate.EstadoCivil)
                    && string.IsNullOrWhiteSpace(employeeForUpdate.Email)
                    && string.IsNullOrWhiteSpace(employeeForUpdate.Direccion)
                    && employeeForUpdate.FechaBaja.HasValue
                    )
                {
                    return BadRequest("Invalid model object");
                }

                var employeeDTO = await _serviceManager.EmployeeServices.UpdateAsync(id, employeeForUpdate);

                return Ok(employeeDTO);
            }
            catch (Exception)
            {
                return BadRequest("Internal server error");
            }
        }
    }
}
