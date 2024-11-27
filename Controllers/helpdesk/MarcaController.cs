using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using noa.Models;

namespace noa.Controllers.helpdesk
{
    public class MarcaController : Controller
    {
        private readonly IConfiguration _configuration;
        public MarcaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index(string buscaid)
        {
            var model = string.IsNullOrEmpty(buscaid) ? ListaDTO().Result : BuscaId(buscaid).Result;
            return View("~/Views/helpdesk/Marca/Index.cshtml", model);
        }

        public async Task<List<MarcaDTO>> ListaDTO()
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

        public async Task<List<MarcaDTO>> BuscaId(string buscaid)
        {
            var client = new HttpClient();
            var request = await client.GetAsync($"{_configuration["Inven:URL"]}/Marca/{buscaid}");
            if (request.IsSuccessStatusCode)
            {
                var jsonResponse = await request.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<MarcaDTO>(jsonResponse);
                return [dto];
            }
            else
            {
                return [];
            }
        }
        public async Task<IActionResult> InsertUpdate(MarcaDTO elementDTO)
        {
            var httpClient = new HttpClient();

            // Verificar el valor del estado
            Console.WriteLine($"Estado recibido: {elementDTO.Activo}");

            if (!string.IsNullOrEmpty(elementDTO.Id))
            {
                // Actualizar registro existente
                var response = await httpClient.PutAsJsonAsync($"{_configuration["Inven:URL"]}/Marca/{elementDTO.Id}", elementDTO);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Marca");
                }
                else
                {
                    // Manejo de error en la actualización
                    Console.WriteLine("Error al actualizar el registro");
                    return RedirectToAction("Index", "Marca");
                }
            }
            else
            {
                // Validar campos antes de crear un nuevo registro
                if (string.IsNullOrEmpty(elementDTO.Nombre))
                {
                    Console.WriteLine("Al menos debe llenar un campo");
                    return RedirectToAction("Index", "Marca");
                }
                else
                {
                    // Crear nuevo registro
                    var request = await httpClient.PostAsJsonAsync($"{_configuration["Inven:URL"]}/Marca", elementDTO);
                    if (request.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Marca");
                    }
                    else
                    {
                        // Manejo de error en la creación
                        Console.WriteLine("Error al crear el registro");
                        return RedirectToAction("Index", "Marca");
                    }
                }
            }
        }
        [HttpDelete]
        public async Task<bool> EliminarMarca(string deleteid)
        {
            var client = new HttpClient();
            var response = await client.DeleteAsync($"{_configuration["Inven:URL"]}/Marca/{deleteid}");
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
    }
}





