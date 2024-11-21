using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using noa.Models;

namespace noa.Controllers
{
    public class PropiedadesController : Controller
    {
        private readonly IConfiguration _configuration;
        public PropiedadesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index(string buscaid)
        {
            var model = string.IsNullOrEmpty(buscaid) ? ListaDTO().Result : BuscaId(buscaid).Result;
            return View("~/Views/Propiedades/Index.cshtml", model);
        }

        public async Task<List<PropiedadesDTO>> ListaDTO()
        {
            var client = new HttpClient();

            // Realiza la solicitud GET
            var response = await client.GetAsync($"{_configuration["Inven:URL"]}/Propiedades");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<PropiedadesDTO>>(jsonResponse);
            }
            else
            {
                return new List<PropiedadesDTO>();
            }
        }

        public async Task<List<PropiedadesDTO>> BuscaId(string buscaid)
        {
            var client = new HttpClient();
            var request = await client.GetAsync($"{_configuration["Inven:URL"]}/Propiedades/{buscaid}");
            if (request.IsSuccessStatusCode)
            {
                var jsonResponse = await request.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<PropiedadesDTO>(jsonResponse);
                return [dto];
            }
            else
            {
                return [];
            }
        }
        public async Task<IActionResult> InsertUpdate(PropiedadesDTO elementDTO)
        {
            if (elementDTO.Id != 0)
            {
                var httpClient = new HttpClient();
                var response = await httpClient.PutAsJsonAsync($"{_configuration["Inven:URL"]}/Propiedades/{elementDTO.Id}", elementDTO);
                if (response.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index", "Propiedades");
                }
                else
                {
                    return RedirectToAction("Index", "Propiedades");
                }
            }
            else
            {
                if (string.IsNullOrEmpty(elementDTO.Propiedad))
                    
                {
                    Console.WriteLine("Al menos debe llenar un campo");
                    var model = ListaDTO().Result;
                    return RedirectToAction("Index", "Propiedades");
                }
                else
                {
                    var client = new HttpClient();
                    var request = await client.PostAsJsonAsync($"{_configuration["Inven:URL"]}/Propiedades", elementDTO);
                    if (request.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Propiedades");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Propiedades");
                    }
                }
            }

        }

        [HttpDelete]
        public async Task<bool> EliminarArea(int deleteid)
        {
            var client = new HttpClient();
            var response = await client.DeleteAsync($"{_configuration["Inven:URL"]}/Propiedades/{deleteid}");
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
        public async Task<List<EnsambleDTO>> ListaEnsamble()
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
