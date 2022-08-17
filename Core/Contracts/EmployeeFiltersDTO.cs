using Microsoft.AspNetCore.Mvc;

namespace Contracts
{
    public class EmployeeFiltersDTO
    {
        public string  ? Nombre { get; set; }

        public string ?  Rfc { get; set; }

        public bool ? lEmployeeUnsuscribed { get; set; }
    }
}
