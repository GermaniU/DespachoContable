using AutoMapper;
using Domain.Repositories;
using Services.Abstractions;

namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IEmployeeServices> _lazyEmployeeServices;

        private readonly Lazy<IPositionService> _lazyPositionService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper _mapper)
        {
            _lazyEmployeeServices = new Lazy<IEmployeeServices>(() => new EmployeeServices(repositoryManager, _mapper));

            _lazyPositionService = new Lazy<IPositionService >(() => new PositionServices(repositoryManager, _mapper));
        }
       
        public IEmployeeServices EmployeeServices => _lazyEmployeeServices.Value;
       
        public IPositionService PositionService => _lazyPositionService.Value;
    }
}
