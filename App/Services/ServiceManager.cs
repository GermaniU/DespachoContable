using DespachoContable.Services.Employee;
using DespachoContable.Services.Position;

namespace DespachoContable.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IEmployeeServices> _lazyEmployeeService;

        private readonly Lazy<IPositionServices> _lazyPositionService;


        public ServiceManager(IConfiguration configuration)
        {
            _lazyEmployeeService = new Lazy<IEmployeeServices>(() => new EmployeeServices(configuration));

            _lazyPositionService = new Lazy<IPositionServices>(() => new PositionServices(configuration));
        }

        public IEmployeeServices EmployeeServices => _lazyEmployeeService.Value;

        public IPositionServices PositionService => _lazyPositionService.Value;
    }
}
