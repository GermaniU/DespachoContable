using Contracts;

namespace DespachoContable.Services.Employee
{
    public interface IEmployeeServices
    {
        Task<IEnumerable<EmployeeDTO>> GetAllAsync();

        Task<IEnumerable<EmployeeDTO>> GetAllAsyncFiltered(EmployeeFiltersDTO employeeFilters);

        Task<EmployeeDTO> GetByIdAsync(Guid employeeId);

        Task<EmployeeDTO> CreateAsync(EmployeeForCreationDTO employeeForCreationDto);

        Task<EmployeeDTO> UpdateAsync(Guid employeeId, EmployeeForUpdateDTO employeeForUpdateDto);

        Task<EmployeeDTO> UnsubscribeAsync(Guid employeeId);
    }
}
