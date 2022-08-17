using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class EmployeeForCreationDTO
    {
        [Required(ErrorMessage = "Capturar Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Capturar Teléfono")]
        public string Telefono { get; set; }

        public DateTime? FechaBaja { get; set; }

        [Required(ErrorMessage = "Capturar Estado Civil")]
        public string EstadoCivil { get; set; }

        [Required(ErrorMessage = "Capturar Dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Capturar Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Capturar Apellido Paterno")]
        public string ApellidoPaterno { get; set; }

        [Required(ErrorMessage = "Capturar Apellido Materno")]
        public string ApellidoMaterno { get; set; }

        [Required(ErrorMessage = "Capturar Fecha Nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "Capturar Género")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "Capturar RFC")]
        public string Rfc { get; set; }

        [Required(ErrorMessage = "Capturar Puesto")]
        public Guid IdPuesto { get; set; }

        [Required(ErrorMessage = "Capturar Fecha Alta")]
        public DateTime FechaAlta { get; set; }
    }
}
