using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using noa.Models;

namespace noa.Controllers.helpdesk
{
    public class CentrocostosController : Controller
    {
        private readonly IConfiguration _configuration;
        public CentrocostosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Acción para manejar la vista principal
        public async Task<IActionResult> Index(string buscaid)
        {
            var model = string.IsNullOrEmpty(buscaid) ? await ListaDTO() : await BuscaId(buscaid);
            return View("~/Views/helpdesk/Centrocostos/Index.cshtml", model);
        }

        [HttpGet]
        public async Task<List<CentroDeCostoDTO>> ListaDTO()
        {
            var client = new HttpClient();
            // Realiza la solicitud GET
            var response = await client.GetAsync($"{_configuration["Inven:URL"]}/CentroDeCosto");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<CentroDeCostoDTO>>(jsonResponse);
            }
            else
            {
                return [];
            }
        }
        public async Task<List<CentroDeCostoDTO>> BuscaId(string buscaid)
        {
            var client = new HttpClient();
            var request = await client.GetAsync($"{_configuration["Inven:URL"]}/CentroDeCosto/{buscaid}");
            if (request.IsSuccessStatusCode)
            {
                var jsonResponse = await request.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<CentroDeCostoDTO>(jsonResponse);
                return [dto];
            }
            else
            {
                return [];
            }
        }
    }

}





