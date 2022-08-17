using AutoMapper;
using Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Repositories;
using Presentation.Controllers;
using Services;
using Services.Config;
using Xunit;

namespace DespachoContableUTest
{
    public class  EmployeeControllerUtest
    {
        private static IMapper? _mapper;

        public EmployeeControllerUtest()
        {
            RegisterMapperProfile();
        }

        private void RegisterMapperProfile()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new AutoMapperProfile()); });

                IMapper mapper = mappingConfig.CreateMapper();

                _mapper = mapper;
            }
        }

        private RepositoryDbContext CreateMockDbContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new RepositoryDbContext(options);

            return dbContext;
        }

        [Fact]
        public async Task GetAllEmployees_Succes_ReturnsEmployees()
        {
            var dbContext = CreateMockDbContext();

            MockDataPositionsBd(dbContext);

            MockDataEmployeeDb(dbContext);

            var SUT = GetInstanceUserController(dbContext);

            var response = await SUT.GetAllEmployees();

            Assert.IsType<OkObjectResult>(response);
        }

        //[Fact]
        //public async Task GetAllEmployeesFiltered_FilterByUnsuscribed_Succes_ReturnsEmployees()
        //{
        //    var dbContext = CreateMockDbContext();

        //    MockDataPositionsBd(dbContext);

        //    MockDataEmployeeDb(dbContext);

        //    var SUT = GetInstanceUserController(dbContext);

        //    var EmployeeFilters = new EmployeeFiltersDTO();

        //    EmployeeFilters.lEmployeeUnsuscribed = true;

        //    var response = await SUT.GetAllEmployeesFiltered(EmployeeFilters);

        //    var data = response as OkObjectResult;

        //    List<EmployeeDTO> employeesDTO = (List<EmployeeDTO>)data.Value;

        //    Assert.Equal(2, employeesDTO.Count());
        //}

        [Fact]
        public async Task GetEmployeesById_Succes_ReturnsEmployee()
        {
            var dbContext = CreateMockDbContext();

            MockDataPositionsBd(dbContext);

            MockDataEmployeeDb(dbContext);

            var SUT = GetInstanceUserController(dbContext);

            var Guid = ToGuid(1);

            var response = await SUT.GetEmployeesById(Guid);

            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public async Task CreateEmployee_Succes_ReturnsEmployee()
        {
            var dbContext = CreateMockDbContext();

            MockDataPositionsBd(dbContext);

            MockDataEmployeeDb(dbContext);

            var SUT = GetInstanceUserController(dbContext);

            EmployeeForCreationDTO employee = CreateEmployeeForCreationDTO(1);

            var response = await SUT.CreateEmployee(employee);

            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public async Task UpdateEmployee_Succes_ReturnsEmployee()
        {
            var dbContext = CreateMockDbContext();

            MockDataPositionsBd(dbContext);

            MockDataEmployeeDb(dbContext);

            var SUT = GetInstanceUserController(dbContext);

            var employeeForUpdate = new EmployeeForUpdateDTO();

            employeeForUpdate.Email = "test@gmail.com";

            Guid employeeId = ToGuid(1);

            var response = await SUT.UpdateEmployee(employeeId, employeeForUpdate);

            var data = response as OkObjectResult;

            EmployeeDTO employeeDTO = (EmployeeDTO) data.Value;

            Assert.Equal(employeeForUpdate.Email,employeeDTO.Email);           
        }

        [Fact]
        public async Task UnsubscribeEmployee_Succes_ReturnsEmployee()
        {
            var dbContext = CreateMockDbContext();

            MockDataPositionsBd(dbContext);

            MockDataEmployeeDb(dbContext);

            var SUT = GetInstanceUserController(dbContext);

            Guid employeeId = ToGuid(1);

            var response = await SUT.UnsubscribeEmployee(employeeId);

            var data = response as OkObjectResult;

            EmployeeDTO employeeDTO = (EmployeeDTO) data.Value;

            Assert.NotNull(employeeDTO.FechaBaja);
        }

        private static EmployeeController GetInstanceUserController(RepositoryDbContext dbContext)
        {
            var managerRepository = new RepositoryManager(dbContext);

            var ManagerServ = new ServiceManager(managerRepository, _mapper);

            var SUT = new EmployeeController(ManagerServ);

            return SUT;
        }

        private static void MockDataEmployeeDb(RepositoryDbContext context)
        {
            for (int i = 1; i <= 5; i++)
            {

               Employee employee  =  CreateEmployee(i);

                context.Employees.Add(employee);
            }

            context.SaveChanges();

            //and then to detach everything 
            foreach (var entity in context.ChangeTracker.Entries())
            {
                entity.State = EntityState.Detached;
            }
        }

        private static EmployeeForCreationDTO CreateEmployeeForCreationDTO(int i)
        {
            var employee = new EmployeeForCreationDTO();

            employee.Nombre = $"Juan {i}";
            employee.ApellidoPaterno = $"Juan {i}";
            employee.ApellidoMaterno = $"Juan {i}";
            employee.FechaNacimiento = DateTime.Now;
            employee.Genero = "Mujer";
            employee.EstadoCivil = "Soltera";
            employee.Rfc = $"UIWDJASKD";
            employee.Direccion = $"Calle 332";
            employee.Email = $"Juangomez ";
            employee.Telefono = $"9998877 ";
            employee.IdPuesto = ToGuid(i);
            employee.FechaAlta = DateTime.Now;

            return employee;
        }

        private static Employee CreateEmployee(int i)
        {
            var employee = new Employee();

            employee.Id = ToGuid(i);
            employee.Nombre = $"Juan {i}";
            employee.ApellidoPaterno = $"Juan {i}";
            employee.ApellidoMaterno = $"Juan {i}";
            employee.FechaNacimiento = DateTime.Now;
            employee.Genero = "Mujer";
            employee.EstadoCivil = "Soltera";
            employee.Rfc = $"UIWDJASKD";
            employee.Direccion = $"Calle 332";
            employee.Email = $"Juangomez ";
            employee.Telefono = $"9998877 ";
            employee.IdPuesto = ToGuid(i);
            employee.FechaAlta = DateTime.Now;
          
            if ((i % 2) == 0)
            {
                employee.FechaBaja = DateTime.Now;
            }

            return employee;
        }

        private static void MockDataPositionsBd(RepositoryDbContext context)
        {
            for (int i = 1; i <= 5; i++)
            {
                var position = new Position();

                position.IdPuesto = ToGuid( i);

                position.Nombre = $"DEV";
               

                context.Positions.Add(position);
            }

            context.SaveChanges();

            //and then to detach everything 
            foreach (var entity in context.ChangeTracker.Entries())
            {
                entity.State = EntityState.Detached;
            }
        }

        private static Guid ToGuid(int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }
    }
}
