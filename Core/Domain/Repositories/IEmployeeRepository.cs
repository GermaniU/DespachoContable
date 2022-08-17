using Domain.Entities;

namespace Domain.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();

        Task<IEnumerable<Employee>> GetAllAsyncFiltered(string Nombre, string Rfc, bool ? lEmployeeUnsuscribe);

        Task<Employee> GetByIdAsync(Guid employeeId);

        void Update(Employee employe);

        void Insert(Employee employee);

        void Remove(Employee employee);
    }
}
