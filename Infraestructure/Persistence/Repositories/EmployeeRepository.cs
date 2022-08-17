using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Common;
using System.Linq.Expressions;

namespace Persistence.Repositories
{
    internal sealed class EmployeeRepository : IEmployeeRepository
    {
        private readonly RepositoryDbContext _dbContext;

        public EmployeeRepository(RepositoryDbContext dbContext) => _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            var employees = (from employee in _dbContext.Employees
                             join position in _dbContext.Positions on employee.IdPuesto equals position.IdPuesto
                             where employee.FechaBaja.HasValue == false
                             select new Employee
                             {
                                 Id = employee.Id,
                                 Nombre = employee.Nombre,
                                 ApellidoPaterno = employee.ApellidoPaterno,
                                 ApellidoMaterno = employee.ApellidoMaterno,
                                 FechaNacimiento = employee.FechaNacimiento,
                                 Genero = employee.Genero,
                                 EstadoCivil = employee.EstadoCivil,
                                 Rfc = employee.Rfc,
                                 Direccion = employee.Direccion,
                                 Email = employee.Email,
                                 Telefono = employee.Telefono,
                                 IdPuesto = employee.IdPuesto,
                                 FechaAlta = employee.FechaAlta,
                                 FechaBaja = employee.FechaBaja,
                                 Position = position
                             });

           return await employees.ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllAsyncFiltered(string Nombre, string Rfc, bool ? lEmployeeUnsuscribe)
        {
            var employeeBd = _dbContext.Employees;
           
            var predicate = PredicateBuilder.True<Employee>();


            if (!string.IsNullOrWhiteSpace(Nombre))
            {
                predicate = predicate.And(c=>c.Nombre == Nombre); 
            }

            if (!string.IsNullOrWhiteSpace(Rfc))
            {
                predicate = predicate.And(c => c.Rfc == Rfc);
            }

            if (lEmployeeUnsuscribe.HasValue)
            {
                if (lEmployeeUnsuscribe.Value)
                {
                    predicate = predicate.And(c => c.FechaBaja.HasValue);
                }
                else
                {
                    predicate = predicate.And(c => c.FechaBaja.HasValue == false);
                }
            }

            var employees = (from employee in employeeBd.Where(predicate)
                             join position in _dbContext.Positions on employee.IdPuesto equals position.IdPuesto
                             select new Employee
                             {
                                 Id = employee.Id,
                                 Nombre = employee.Nombre,
                                 ApellidoPaterno = employee.ApellidoPaterno,
                                 ApellidoMaterno = employee.ApellidoMaterno,
                                 FechaNacimiento = employee.FechaNacimiento,
                                 Genero = employee.Genero,
                                 EstadoCivil = employee.EstadoCivil,
                                 Rfc = employee.Rfc,
                                 Direccion = employee.Direccion,
                                 Email = employee.Email,
                                 Telefono = employee.Telefono,
                                 IdPuesto = employee.IdPuesto,
                                 FechaAlta = employee.FechaAlta,
                                 FechaBaja = employee.FechaBaja,
                                 Position = position
                             });

            return await employees.ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(Guid employeeId)
        {
            var employeeEntity = (from employee in _dbContext.Employees
                             join position in _dbContext.Positions on employee.IdPuesto equals position.IdPuesto
                             where employee.Id == employeeId
                             select new Employee
                             {
                                 Id = employee.Id,
                                 Nombre = employee.Nombre,
                                 ApellidoPaterno = employee.ApellidoPaterno,
                                 ApellidoMaterno = employee.ApellidoMaterno,
                                 FechaNacimiento = employee.FechaNacimiento,
                                 Genero = employee.Genero,
                                 EstadoCivil = employee.EstadoCivil,
                                 Rfc = employee.Rfc,
                                 Direccion = employee.Direccion,
                                 Email = employee.Email,
                                 Telefono = employee.Telefono,
                                 IdPuesto = employee.IdPuesto,
                                 FechaAlta = employee.FechaAlta,
                                 FechaBaja = employee.FechaBaja,
                                 Position = position
                             });

            return await employeeEntity.FirstAsync();

        }

        public void Insert(Employee employee)
        {
            _dbContext.Entry(employee).State = EntityState.Added;

            _dbContext.Employees.Add(employee);
        }

        public void Remove(Employee employee)
        {
            _dbContext.Entry(employee).State = EntityState.Deleted;

            _dbContext.Employees.Remove(employee);
        }

        public void Update(Employee employe)
        {
            _dbContext.Entry(employe).State = EntityState.Modified;

            _dbContext.Update(employe);
        }
    }
}
