using Contracts;
using DespachoContable.Common;
using System;
using System.IO;
using System.Text;

namespace DespachoContable.Services.Employee
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly string apiBaseUrl;

        private HttpClient client;

        public EmployeeServices(IConfiguration configuration)
        {
            apiBaseUrl = configuration.GetValue<string>("WebAPIBaseUrl");

            client = new HttpClient();
        }

        public async Task<EmployeeDTO> CreateAsync(EmployeeForCreationDTO employeeForCreationDto)
        {
            string path = $"{apiBaseUrl}/Employee";
            EmployeeDTO employeeDto = null;

            HttpResponseMessage response = await client.PostAsJsonAsync(path, employeeForCreationDto);

            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            if (response.IsSuccessStatusCode)
            {
                employeeDto = await response.Content.ReadAsAsync<EmployeeDTO>();
            }

            return employeeDto;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllAsync()
        {
            string path = $"{apiBaseUrl}/Employee";

            IEnumerable<EmployeeDTO> employees = null;

            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                employees = await response.Content.ReadAsAsync<IEnumerable<EmployeeDTO>>();
            }

            return employees;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllAsyncFiltered(EmployeeFiltersDTO employeeFilters)
        {

             string path = $"{apiBaseUrl}/EmployeeFilter/?Nombre={employeeFilters.Nombre}&&Rfc={employeeFilters.Rfc}&lEmployeeUnsuscribed={employeeFilters.lEmployeeUnsuscribed}";

            IEnumerable<EmployeeDTO> employees = null;

            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                employees = await response.Content.ReadAsAsync<IEnumerable<EmployeeDTO>>();
            }

            return employees;
        }

        public async Task<EmployeeDTO> GetByIdAsync(Guid employeeId)
        {
            string path = $"{apiBaseUrl}/Employee/{employeeId}";

            IEnumerable<EmployeeDTO> employees = null;

            EmployeeDTO employeeDto = null;

            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                employeeDto = await response.Content.ReadAsAsync<EmployeeDTO>();
            }

            return employeeDto;
        }

        public async Task<EmployeeDTO> UnsubscribeAsync(Guid employeeId)
        {
            string path = $"{apiBaseUrl}/Employee/?id={employeeId}";

            EmployeeDTO employeeDto = null;

            StringContent content
                   = new StringContent("");

            HttpResponseMessage response = await client.PatchAsync(path, content);

            if (response.IsSuccessStatusCode)
            {
                employeeDto = await response.Content.ReadAsAsync<EmployeeDTO>();
            }

            return employeeDto;
        }

        public async Task<EmployeeDTO> UpdateAsync(Guid employeeId, EmployeeForUpdateDTO employeeForUpdateDto)
        {
            string path = $"{apiBaseUrl}/Employee/?id={employeeId}";

            EmployeeDTO employeeDto = null;

            HttpResponseMessage response = await client.PutAsJsonAsync(path, employeeForUpdateDto);

            response.EnsureSuccessStatusCode();

            // Deserialize the updated employee from the response body.
            employeeDto = await response.Content.ReadAsAsync<EmployeeDTO>();

            return employeeDto;
        }
    }
}
