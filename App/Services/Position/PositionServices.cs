using Contracts;

namespace DespachoContable.Services.Position
{
    public class PositionServices : IPositionServices
    {
        private readonly string apiBaseUrl;

        private HttpClient client;

        public PositionServices(IConfiguration configuration)
        {
            apiBaseUrl = configuration.GetValue<string>("WebAPIBaseUrl");

            client = new HttpClient();
        }

        public async Task<IEnumerable<PositionDTO>> GetAllAsync()
        {
            string path = $"{apiBaseUrl}/Position";

            IEnumerable<PositionDTO> positions = null;

            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                positions = await response.Content.ReadAsAsync<IEnumerable<PositionDTO>>();
            }

            return positions;
        }

    }
}
