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

        public async Task<IActionResult> Index()
        {
            var model = await ListaDTO();
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
    }

}





