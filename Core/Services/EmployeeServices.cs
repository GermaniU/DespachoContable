using AutoMapper;
using Contracts;
using Domain.Entities;
using Domain.Exceptions.Employee;
using Domain.Exceptions.Position;
using Domain.Repositories;
using Services.Abstractions;

namespace Services
{
    internal sealed class EmployeeServices : IEmployeeServices
    {
        private readonly IRepositoryManager _repositoryManager;

        private readonly IMapper _mapper;

        public EmployeeServices(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllAsync()
        {
            var employee = await _repositoryManager.EmployeeRepository.GetAllAsync();

            var employeeDto = _mapper.Map<IEnumerable<EmployeeDTO>>(employee); //convert employee object to EmployeeDTO;

            return employeeDto;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllAsyncFiltered(EmployeeFiltersDTO employeeFilters )
        {
            var employee = await _repositoryManager.EmployeeRepository.GetAllAsyncFiltered(employeeFilters.Nombre, employeeFilters.Rfc, employeeFilters.lEmployeeUnsuscribed);

            var employeeDto = _mapper.Map<IEnumerable<EmployeeDTO>>(employee); //convert employee object to EmployeeDTO;

            return employeeDto;
        }

        public async Task<EmployeeDTO> GetByIdAsync(Guid employeeId)
        {
            var employee = await _repositoryManager.EmployeeRepository.GetByIdAsync(employeeId);
           
            if (employee is null)
            {
                throw new EmployeeNotFoundException(employeeId);
            }
           
            var employeeDto = _mapper.Map<EmployeeDTO>(employee);

            return employeeDto;
        }

        public async Task<EmployeeDTO> CreateAsync(EmployeeForCreationDTO employeeForCreationDto)
        {
            if (employeeForCreationDto is null)
            {
                throw new EmployeeDataEmpty();
            }

            var employee = _mapper.Map<Employee>(employeeForCreationDto);

            _repositoryManager.EmployeeRepository.Insert(employee);

            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            var employeeDTO = await GetByIdAsync(employee.Id);

            return employeeDTO;
        }

        public async Task<EmployeeDTO> UpdateAsync(Guid employeeId, EmployeeForUpdateDTO employeeForUpdateDto)
        {
            var employee = await _repositoryManager.EmployeeRepository.GetByIdAsync(employeeId);

            if (employee is null)
            {
                throw new EmployeeNotFoundException(employeeId);
            }

            var position = await _repositoryManager.PositionRepository.GetByIdAsync(employeeForUpdateDto.IdPuesto);

            if (position is null)
            {
                throw new PositionNotFoundException(employeeForUpdateDto.IdPuesto);
            }

            AssignDataEmployeeForUpdate(employeeForUpdateDto, employee, position);

            _repositoryManager.EmployeeRepository.Update(employee);

            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<EmployeeDTO>(employee);
        }

        public async Task<EmployeeDTO> UnsubscribeAsync(Guid employeeId)
        {
            var employee = await _repositoryManager.EmployeeRepository.GetByIdAsync(employeeId);

            if (employee is null)
            {
                throw new EmployeeNotFoundException(employeeId);
            }

            employee.FechaBaja = DateTime.Now;

             _repositoryManager.EmployeeRepository.Update(employee);

            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<EmployeeDTO>(employee);
        }

        private void AssignDataEmployeeForUpdate(EmployeeForUpdateDTO EmployeeForUpdateDto, Employee employee, Position position)
        {
            employee.Email = EmployeeForUpdateDto.Email;

            employee.Telefono = EmployeeForUpdateDto.Telefono;

            employee.EstadoCivil = EmployeeForUpdateDto.EstadoCivil;

            employee.Direccion = EmployeeForUpdateDto.Direccion;

            employee.FechaBaja = EmployeeForUpdateDto.FechaBaja;

            employee.IdPuesto = EmployeeForUpdateDto.IdPuesto;

            employee.Position = position;

        }

    }
}
