using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using noa.Models;
using System.Globalization;

namespace noa.Controllers.helpdesk
{
    public class CentrocostosController : Controller
    {
        private readonly IConfiguration _configuration;
        public CentrocostosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(string buscaId)
        {
            //var model = string.IsNullOrEmpty(buscaId) ? await ListaDTO() : await BuscaId(buscaId);
            var model = string.IsNullOrEmpty(buscaId) ? await ListaDTO() : await BuscaId(buscaId);
            return View("~/Views/helpdesk/Centrocostos/Index.cshtml", model);
        }

        [HttpGet]
        //public async Task<List<CentroDeCostoDTO>> ListaDTO()
        public async Task<CentroCosto> ListaDTO()
        {
            var client = new HttpClient();

            // Realiza la solicitud GET
            //var response = await client.GetAsync($"{_configuration["Inven:URL"]}/CentroDeCosto");
            var response = await client.GetAsync($"{_configuration["Inven:URL"]}/CentroDeCosto/linq combinado");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                //return JsonConvert.DeserializeObject<List<CentroDeCostoDTO>>(jsonResponse);
                return JsonConvert.DeserializeObject<CentroCosto>(jsonResponse);
            }
            else
            {
                return null;

            }
        }
        //public async Task<List<CentroDeCostoDTO>> BuscaId(string buscaid)
        public async Task<CentroCosto> BuscaId(string buscaid)
        {
            var client = new HttpClient();
            var request = await client.GetAsync($"{_configuration["Inven:URL"]}/CentroDeCosto{buscaid}");
            if (request.IsSuccessStatusCode)
            {
                var jsonResponse = await request.Content.ReadAsStringAsync();
                //var dto = JsonConvert.DeserializeObject<CentroDeCostoDTO>(jsonResponse);    
                var dto = JsonConvert.DeserializeObject<CentroCosto>(jsonResponse);    
                return dto;
            }
            else
            {
                return null;
            }
        }
    }

}





