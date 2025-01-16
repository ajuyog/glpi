using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using noa.Models;
using System.Net.Sockets;


namespace noa.Controllers
{
    public class MesaAyudaController : Controller
    {
        private readonly IConfiguration _configuration;

        public MesaAyudaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(string buscaId)
        {
            var model = string.IsNullOrEmpty(buscaId) ? await ListaDTO() : await BuscaId(buscaId);
            return View("~/Views/MesaAyuda/Index.cshtml", model);
        }

        public async Task<List<MesadeAyudaDTO>> ListaDTO()
        {
            var client = new HttpClient();

            // Realiza la solicitud GET
            var response = await client.GetAsync($"{_configuration["Inven:Prueba"]}/Solicitud/Linq");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<MesadeAyudaDTO>>(jsonResponse);
            }
            else
            {
                return new List<MesadeAyudaDTO>();

            }
        }

        public async Task<List<MesadeAyudaDTO>> BuscaId(string buscaid)
        {
            var client = new HttpClient();
            var request = await client.GetAsync($"{_configuration["Inven:Prueba"]}/Solicitud/Linq{buscaid}");
            if (request.IsSuccessStatusCode)
            {
                var jsonResponse = await request.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<MesadeAyudaDTO>(jsonResponse);
                return [dto];
            }
            else
            {
                return [];
            }
        }

        [HttpGet]
        public async Task<List<MesadeAyudaDTO>> ListaSolicitud()
        {
            var client = new HttpClient();

            // Realiza la solicitud GET
            var response = await client.GetAsync($"{_configuration["Inven:URL"]}/Solicitud/Linq");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<MesadeAyudaDTO>>(jsonResponse);
            }
            else
            {
                return [];
            }
        }

    }
} 
