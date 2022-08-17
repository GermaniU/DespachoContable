using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class EmployeeForUpdateDTO
    {

        [Required(ErrorMessage = "Capturar Estado Civil")]
        public string EstadoCivil { get; set; }

        [Required(ErrorMessage = "Capturar Dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Capturar Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Capturar Teléfono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Capturar Puesto")]
        public Guid IdPuesto { get; set; }

        public DateTime? FechaBaja { get; set; }

    }
}
