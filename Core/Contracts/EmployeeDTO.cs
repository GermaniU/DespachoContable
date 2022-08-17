using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class EmployeeDTO
    {
        public Guid Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string ApellidoPaterno { get; set; }

        [Required]
        public string ApellidoMaterno { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        public string Genero { get; set; }

        [Required]
        public string EstadoCivil { get; set; }

        [Required]
        public string Rfc { get; set; }

        [Required]
        public string Direccion { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        public DateTime FechaAlta { get; set; }

       
        public Guid IdPuesto { get; set; }

        public DateTime? FechaBaja { get; set; }

        public string Puesto { get; set; }
    }
}
