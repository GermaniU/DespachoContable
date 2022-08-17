using DespachoContable.Services.Employee;
using DespachoContable.Services.Position;

namespace DespachoContable.Services
{
    public interface IServiceManager
    {
        IEmployeeServices EmployeeServices { get; }

        IPositionServices PositionService { get; }

    }
}