using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class PositionForPersitenceDto
    {
        [Required]
        public string Nombre { get; set; }
    }
}
