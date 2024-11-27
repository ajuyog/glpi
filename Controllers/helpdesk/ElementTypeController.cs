using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using noa.Models;

namespace noa.Controllers.helpdesk
{
    public class ElementTypeController : Controller
    {
        private readonly ILogger<ElementTypeController> _logger;
        private readonly IConfiguration _configuration;
        public ElementTypeController(ILogger<ElementTypeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public IActionResult Index(string buscaid)
        {
            var model = string.IsNullOrEmpty(buscaid) ? ListaDTO().Result : BuscaId(buscaid).Result;
            return View("~/Views/helpdesk/ElementType/Index.cshtml", model);
        }
        public async Task<List<ElementTypeDTO>> ListaDTO()
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
        public async Task<List<ElementTypeDTO>> BuscaId(string buscaid)
        {
            var client = new HttpClient();
            var request = await client.GetAsync($"{_configuration["Inven:URL"]}/ElementType/{buscaid}");
            if (request.IsSuccessStatusCode)
            {
                var jsonResponse = await request.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<ElementTypeDTO>(jsonResponse);
                return [dto];
            }
            else
            {
                return [];
            }
        }
        public async Task<IActionResult> InsertUpdate(ElementTypeDTO elementDTO)
        {
            if (elementDTO.Id != 0)
            {
                var httpClient = new HttpClient();
                var response = await httpClient.PutAsJsonAsync($"{_configuration["Inven:URL"]}/ElementType/{elementDTO.Id}", elementDTO);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "ElementType");
                }
                else
                {
                    return RedirectToAction("Index", "ElementType");
                }
            }
            else
            {
                if (string.IsNullOrEmpty(elementDTO.Nombre) && elementDTO.IdElementType == 0)
                {
                    Console.WriteLine("Al menos debe llenar un campo");
                    return RedirectToAction("Index", "ElementType");
                }
                else
                {
                    var client = new HttpClient();
                    var request = await client.PostAsJsonAsync($"{_configuration["Inven:URL"]}/ElementType", elementDTO);
                    if (request.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index", "ElementType");
                    }
                    else
                    {
                        return RedirectToAction("Index", "ElementType"); ;
                    }
                }
            }
        }
        [HttpDelete]
        public async Task<bool> EliminarElemento(string deleteid)
        {
            var client = new HttpClient();
            var response = await client.DeleteAsync($"{_configuration["Inven:URL"]}/ElementType/{deleteid}");
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

        public async Task<List<ElementTypeDTO>> ListaTipoelemento()
        {
            var client = new HttpClient();

            // Realiza la solicitud GET
            var response = await client.GetAsync($"{_configuration["Inven:URL"]}/ELementType");

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

    }
}

