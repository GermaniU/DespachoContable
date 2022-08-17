namespace Domain.Repositories
{
    public interface IRepositoryManager
    {
        IEmployeeRepository EmployeeRepository{ get; }

        IPositionRepository PositionRepository { get; }

        IUnitOfWork UnitOfWork { get; }
    }
}
