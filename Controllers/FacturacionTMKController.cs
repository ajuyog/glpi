using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using noa.Models;

namespace noa.Controllers
{
    public class FacturacionTMKController : Controller
    {
        private readonly IConfiguration _configuration;
        public FacturacionTMKController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index(string buscaid)
        {
            var model = string.IsNullOrEmpty(buscaid) ? ListaDTO().Result : BuscaId(buscaid).Result;
            return View("~/Views/FacturaciónTMK/Index.cshtml", model);
        }

        public async Task<List<FacturacionTMKDTO>> ListaDTO()
        {
            var client = new HttpClient();

            // Realiza la solicitud GET
            var response = await client.GetAsync($"{_configuration["Inven:URL"]}/FacturacionTMK");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<FacturacionTMKDTO>>(jsonResponse);
            }
            else
            {
                return [];
            }
        }

        public async Task<List<FacturacionTMKDTO>> BuscaId(string buscaid)
        {
            var client = new HttpClient();
            var request = await client.GetAsync($"{_configuration["Inven:URL"]}/FacturacionTMK/{buscaid}");
            if (request.IsSuccessStatusCode)
            {
                var jsonResponse = await request.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<FacturacionTMKDTO>(jsonResponse);
                return [dto];
            }
            else
            {
                return [];
            }
        }
        public async Task<IActionResult> InsertUpdate(FacturacionTMKDTO elementDTO)
        {
            if (elementDTO.Id != 0)
            {
                var httpClient = new HttpClient();
                var response = await httpClient.PutAsJsonAsync($"{_configuration["Inven:URL"]}/FacturacionTMK/{elementDTO.Id}", elementDTO);
                if (response.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index", "FacturacionTMK");
                }
                else
                {
                    return RedirectToAction("Index", "FacturacionTMK");
                }
            }
            else
            {
                if (elementDTO.VlrNeto <= 0 || elementDTO.IdEnsamble <= 0 ||
                    string.IsNullOrEmpty(elementDTO.Descripcion) || elementDTO.Fecha == DateOnly.MinValue)

                {
                    Console.WriteLine("Al menos debe llenar un campo");
                    return RedirectToAction("Index", "FacturacionTMK");
                }
                else
                {
                    var client = new HttpClient();
                    var request = await client.PostAsJsonAsync($"{_configuration["Inven:URL"]}/FacturacionTMK", elementDTO);
                    if (request.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "FacturacionTMK");
                    }
                    else
                    {
                        return RedirectToAction("Index", "FacturacionTMK");
                    }
                }
            }

        }

        [HttpDelete]
        public async Task<bool> EliminarFactura(int deleteid)
        {
            var client = new HttpClient();
            var response = await client.DeleteAsync($"{_configuration["Inven:URL"]}/FacturacionTMK/{deleteid}");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Área eliminada exitosamente.");
                return true;
            }
            else
            {
                Console.WriteLine("Error al eliminar el área.");
                return false;
            }
        }
        [HttpGet]
        public async Task<List<EnsambleDTO>> ListaIdensamble()
        {
            var client = new HttpClient();

            // Realiza la solicitud GET
            var response = await client.GetAsync($"{_configuration["Inven:URL"]}/Ensamble");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<EnsambleDTO>>(jsonResponse);
            }
            else
            {
                return [];
            }
        }

    }
}
