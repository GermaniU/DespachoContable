using Domain.Repositories;

namespace Persistence.Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {

        private readonly Lazy<EmployeeRepository> _lazyEmployeeRepository;

        private readonly Lazy<PositionRepository> _lazyPositionRepository;

        private readonly Lazy<UnitOfWork> _lazyIUnitOfWork;


        public RepositoryManager(RepositoryDbContext repositoryDbContext)
        {
            _lazyEmployeeRepository = new Lazy<EmployeeRepository>(() => new EmployeeRepository(repositoryDbContext));

            _lazyPositionRepository = new Lazy<PositionRepository>(() => new PositionRepository(repositoryDbContext));

            _lazyIUnitOfWork = new Lazy<UnitOfWork>(() => new UnitOfWork(repositoryDbContext));

        }

        public IEmployeeRepository EmployeeRepository => _lazyEmployeeRepository.Value;

        public IPositionRepository PositionRepository => _lazyPositionRepository.Value;

        public IUnitOfWork UnitOfWork => _lazyIUnitOfWork.Value;
    }
}
