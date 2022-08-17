namespace Domain.Exceptions.Employee
{
    public class EmployeeDataEmpty : BadRequestException
    {
        public EmployeeDataEmpty() 
            : base("Ocurrió un error al capturar los datos.")
        {
        }
    }
}
