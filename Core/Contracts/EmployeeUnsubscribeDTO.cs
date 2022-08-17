using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class EmployeeUnsubscribeDTO
    {
        [Required]
        public DateTime FechaBaja { get; set; }
    }
}
