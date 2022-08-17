using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public  class PositionDTO
    {
        public Guid IdPuesto { get; set; }

        public string Nombre { get; set; }
    }
}
