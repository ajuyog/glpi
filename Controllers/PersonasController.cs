using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using noa.Models;

namespace noa.Controllers
{
    public class PersonasController : Controller
    {
        private readonly IConfiguration _configuration;
        public PersonasController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index(string buscaid)
        {
            var model = string.IsNullOrEmpty(buscaid) ? ListaDTO().Result : BuscaId(buscaid).Result;
            return View("~/Views/Persona/Index.cshtml", model);
        }

        public async Task<List<PersonasDTO>> ListaDTO()
        {
            var client = new HttpClient();

            // Realiza la solicitud GET
            var response = await client.GetAsync($"{_configuration["Inven:URL"]}/Persona");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<PersonasDTO>>(jsonResponse);
            }
            else
            {
                return [];
            }
        }

        public async Task<List<PersonasDTO>> BuscaId(string buscaid)
        {
            var client = new HttpClient();
            var request = await client.GetAsync($"{_configuration["Inven:URL"]}/Persona/{buscaid}");
            if (request.IsSuccessStatusCode)
            {
                var jsonResponse = await request.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<PersonasDTO>(jsonResponse);
                return [dto];
            }
            else
            {
                return [];
            }
        }
        public async Task<IActionResult> InsertUpdate(PersonasDTO elementDTO)
        {
            var httpClient = new HttpClient();

            // Verificar el valor del estado
            Console.WriteLine($"Estado recibido: {elementDTO.Estado}");

            if (!string.IsNullOrEmpty(elementDTO.Id))
            {
                // Actualizar registro existente
                var response = await httpClient.PutAsJsonAsync($"{_configuration["Inven:URL"]}/Persona/{elementDTO.Id}", elementDTO);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Personas");
                }
                else
                {
                    // Manejo de error en la actualización
                    Console.WriteLine("Error al actualizar el registro");
                    return RedirectToAction("Index", "Personas");
                }
            }
            else
            {
                // Validar campos antes de crear un nuevo registro
                if (string.IsNullOrEmpty(elementDTO.UserId) || elementDTO.IdArea <= 0 || string.IsNullOrEmpty(elementDTO.Identificacion))
                {
                    Console.WriteLine("Al menos debe llenar un campo");
                    return RedirectToAction("Index", "Personas");
                }
                else
                {
                    // Crear nuevo registro
                    var request = await httpClient.PostAsJsonAsync($"{_configuration["Inven:URL"]}/Persona", elementDTO);
                    if (request.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Personas");
                    }
                    else
                    {
                        // Manejo de error en la creación
                        Console.WriteLine("Error al crear el registro");
                        return RedirectToAction("Index", "Personas");
                    }
                }
            }
        }
        [HttpDelete]
        public async Task<bool> EliminarArea(string deleteid)
        {
            var client = new HttpClient();
            var response = await client.DeleteAsync($"{_configuration["Inven:URL"]}/Persona/{deleteid}");
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
        public async Task<List<AreaDTO>> List()
        {
            var client = new HttpClient();

            // Realiza la solicitud GET
            var response = await client.GetAsync($"{_configuration["Inven:URL"]}/Area");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<AreaDTO>>(jsonResponse);
            }
            else
            {
                return [];
            }
        }
        [HttpGet]
        public async Task<List<AspnetUsers>> ListaPersonas()
        {
            var client = new HttpClient();

            // Realiza la solicitud GET
            var response = await client.GetAsync($"{_configuration["Inven:URL"]}/AspnetUsers");

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



