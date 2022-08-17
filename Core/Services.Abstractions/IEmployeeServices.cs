using Contracts;

namespace Services.Abstractions
{
    public interface IEmployeeServices
    {
        Task<IEnumerable<EmployeeDTO>> GetAllAsync();

        Task<IEnumerable<EmployeeDTO>> GetAllAsyncFiltered(EmployeeFiltersDTO employeeFilters);

        Task<EmployeeDTO> GetByIdAsync(Guid employeeId);

        Task<EmployeeDTO> CreateAsync(EmployeeForCreationDTO employeeForCreationDto);

        Task<EmployeeDTO> UpdateAsync(Guid employeeId, EmployeeForUpdateDTO employeeForCreationDto);

        Task<EmployeeDTO> UnsubscribeAsync(Guid employeeId);
    }
}
