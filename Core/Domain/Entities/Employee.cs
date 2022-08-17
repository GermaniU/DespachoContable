using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Employee
    {
        [Key]
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
        public string Email { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        public Guid IdPuesto;

        [Required]
        public DateTime FechaAlta { get; set; }

        public DateTime? FechaBaja { get; set; } = null;

        [ForeignKey("IdPuesto")]
        public virtual Position Position { get; set; }

    }
}
