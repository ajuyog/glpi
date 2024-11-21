using Newtonsoft.Json;
using noa.Models;

namespace noa.Controllers
{
    public class AspnetusersController
    {

        private readonly IConfiguration _configuration;
        public AspnetusersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<AspnetUsers>> ListaDTO()
        {
            var client = new HttpClient();

            // Realiza la solicitud GET
            var response = await client.GetAsync($"{_configuration["Inven:URL"]}/aspnetusers");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<AspnetUsers>>(jsonResponse);
            }
            else
            {
                return [];
            }
        }
    }
}
