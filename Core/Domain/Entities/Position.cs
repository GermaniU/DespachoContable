using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Position
    {
        [Key]
        public Guid IdPuesto { get; set; }

        [Required]
        public string Nombre { get; set; }

    }
}
