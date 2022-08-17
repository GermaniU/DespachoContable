using System.ComponentModel.DataAnnotations;

namespace DespachoContable.Models
{
    public class EmployeeForUpdateViewModel
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Genero { get; set; }

        [Required(ErrorMessage = "Capturar Estado Civil")]
        public string EstadoCivil { get; set; }

        public string Rfc { get; set; }

        [Required(ErrorMessage = "Capturar Dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Capturar Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Capturar Teléfono")]
        public string Telefono { get; set; }

        public DateTime FechaAlta { get; set; }

        [Required(ErrorMessage = "Capturar Puesto")]
        public Guid IdPuesto { get; set; }

        public DateTime? FechaBaja { get; set; }
    }
}
