namespace DespachoContable.Models
{
    public class EmployeeViewModel
    {
        public Guid Id { get; set; }

        public string NombreCompleto { get; set; }

        public string Email { get; set; }

        public string Puesto { get; set; }

        public string Rfc { get; set; }

        public DateTime FechaAlta { get; set; }

    }
}
