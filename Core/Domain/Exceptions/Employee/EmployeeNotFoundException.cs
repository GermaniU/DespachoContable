namespace Domain.Exceptions.Employee
{
    public class EmployeeNotFoundException : NotFoundException
    {
        public EmployeeNotFoundException(Guid empleadoId) 
            : base($"El empleado con el identificador {empleadoId} no se encuentra.")
        {
        }
    }
}
