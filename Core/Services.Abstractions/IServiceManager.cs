namespace Services.Abstractions
{
    public interface IServiceManager
    {
        IEmployeeServices EmployeeServices { get; }

        IPositionService PositionService { get; }
    }
}
