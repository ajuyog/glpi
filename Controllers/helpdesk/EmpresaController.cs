using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using noa.Models;

namespace noa.Controllers.helpdesk
{
    public class EmpresaController : Controller
    {
        private readonly IConfiguration _configuration;
        public EmpresaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index(string buscaid)
        {
            var model = string.IsNullOrEmpty(buscaid) ? ListaDTO().Result : BuscaId(buscaid).Result;
            return View("~/Views/helpdesk/Empresa/Index.cshtml", model);
        }

        public async Task<List<EmpresaDTO>> ListaDTO()
        {
            var client = new HttpClient();

            // Realiza la solicitud GET
            var response = await client.GetAsync($"{_configuration["Inven:URL"]}/Empresa");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<EmpresaDTO>>(jsonResponse);
            }
            else
            {
                return [];
            }
        }

        public async Task<List<EmpresaDTO>> BuscaId(string buscaid)
        {
            var client = new HttpClient();
            var request = await client.GetAsync($"{_configuration["Inven:URL"]}/Empresa/{buscaid}");
            if (request.IsSuccessStatusCode)
            {
                var jsonResponse = await request.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<EmpresaDTO>(jsonResponse);
                return [dto];
            }
            else
            {
                return [];
            }
        }
        public async Task<IActionResult> InsertUpdate(EmpresaDTO elementDTO)
        {
            var httpClient = new HttpClient();


            if (!string.IsNullOrEmpty(elementDTO.Id))
            {
                // Actualizar registro existente
                var response = await httpClient.PutAsJsonAsync($"{_configuration["Inven:URL"]}/Empresa/{elementDTO.Id}", elementDTO);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Empresa");
                }
                else
                {
                    // Manejo de error en la actualización
                    Console.WriteLine("Error al actualizar el registro");
                    return RedirectToAction("Index", "Empresa");
                }
            }
            else
            {
                // Validar campos antes de crear un nuevo registro
                if (string.IsNullOrEmpty(elementDTO.Nombre) || string.IsNullOrEmpty(elementDTO.NitEmpresa))
                {
                    Console.WriteLine("Al menos debe llenar un campo");
                    return RedirectToAction("Index", "Empresa");
                }
                else
                {
                    // Crear nuevo registro
                    var request = await httpClient.PostAsJsonAsync($"{_configuration["Inven:URL"]}/Empresa", elementDTO);
                    if (request.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Empresa");
                    }
                    else
                    {
                        // Manejo de error en la creación
                        Console.WriteLine("Error al crear el registro");
                        return RedirectToAction("Index", "Empresa");
                    }
                }
            }
        }
        public async Task<bool> EliminarEmpresa(string deleteid)
        {
            var client = new HttpClient();
            var response = await client.DeleteAsync($"{_configuration["Inven:URL"]}/Empresa/{deleteid}");
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
