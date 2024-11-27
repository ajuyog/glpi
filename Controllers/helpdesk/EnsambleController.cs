using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using noa.Models;

namespace noa.Controllers.helpdesk
{
    public class EnsambleController : Controller
    {
        private readonly IConfiguration _configuration;
        public EnsambleController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index(string buscaid)
        {
            var model = string.IsNullOrEmpty(buscaid) ? ListaDTO().Result : BuscaId(buscaid).Result;
            return View("~/Views/helpdesk/Ensamble/Index.cshtml", model);
        }

        public async Task<List<EnsambleDTO>> ListaDTO()
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

        public async Task<List<EnsambleDTO>> BuscaId(string buscaid)
        {
            var client = new HttpClient();
            var request = await client.GetAsync($"{_configuration["Inven:URL"]}/Ensamble/{buscaid}");
            if (request.IsSuccessStatusCode)
            {
                var jsonResponse = await request.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<EnsambleDTO>(jsonResponse);
                return [dto];
            }
            else
            {
                return [];
            }
        }
        public async Task<IActionResult> InsertUpdate(EnsambleDTO elementDTO)
        {
            if (elementDTO.Id != 0)
            {
                var httpClient = new HttpClient();
                var response = await httpClient.PutAsJsonAsync($"{_configuration["Inven:URL"]}/Ensamble/{elementDTO.Id}", elementDTO);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Ensamble");
                }
                else
                {
                    return RedirectToAction("Index", "Ensamble");
                }
            }
            else
            {
                if (elementDTO.IdElementType <= 0 ||
                    elementDTO.IdMarca <= 0 || string.IsNullOrEmpty(elementDTO.NumeroSerial) ||
                    string.IsNullOrEmpty(elementDTO.Descripcion))
                {
                    Console.WriteLine("Al menos debe llenar un campo");
                    return RedirectToAction("Index", "Ensamble");
                }
                else
                {
                    var client = new HttpClient();
                    var request = await client.PostAsJsonAsync($"{_configuration["Inven:URL"]}/Ensamble", elementDTO);
                    if (request.IsSuccessStatusCode)
                    {
                        //var re = await request.Content.ReadAsStringAsync();
                        return RedirectToAction("Index", "Ensamble"); ;
                    }
                    else
                    {
                        return RedirectToAction("Index", "Ensamble"); ;
                    }
                }
            }

        }

        [HttpDelete]
        public async Task<bool> EliminarEnsamble(string deleteid)
        {
            var client = new HttpClient();
            var response = await client.DeleteAsync($"{_configuration["Inven:URL"]}/Ensamble/{deleteid}");
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
        public async Task<List<ElementTypeDTO>> ListatipoElement()
        {
            var client = new HttpClient();

            // Realiza la solicitud GET
            var response = await client.GetAsync($"{_configuration["Inven:URL"]}/ElementType");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ElementTypeDTO>>(jsonResponse);
            }
            else
            {
                return [];
            }
        }
        [HttpGet]
        public async Task<List<MarcaDTO>> ListaMarca()
        {
            var client = new HttpClient();

            // Realiza la solicitud GET
            var response = await client.GetAsync($"{_configuration["Inven:URL"]}/Marca");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<MarcaDTO>>(jsonResponse);
            }
            else
            {
                return [];
            }
        }

    }
}




