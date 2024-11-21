using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using noa.Models;

namespace noa.Controllers
{
    public class AsignacionController : Controller
    {
            private readonly IConfiguration _configuration;
            public AsignacionController(IConfiguration configuration)
            {
                _configuration = configuration;
            }
            public IActionResult Index(string buscaid)
            {
                var model = string.IsNullOrEmpty(buscaid) ? ListaDTO().Result : BuscaId(buscaid).Result;
                return View("~/Views/Asignacion/Index.cshtml", model);
            }

            public async Task<List<AsignacionDTO>> ListaDTO()
            {
                var client = new HttpClient();

                // Realiza la solicitud GET
                var response = await client.GetAsync($"{_configuration["Inven:URL"]}/Asignacion");
                
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<AsignacionDTO>>(jsonResponse);
                }
                else
                {
                    return [];
                }
            }

            public async Task<List<AsignacionDTO>> BuscaId(string buscaid)
            {
                var client = new HttpClient();
                var request = await client.GetAsync($"{_configuration["Inven:URL"]}/Asignacion/{buscaid}");
                if (request.IsSuccessStatusCode)
                {
                    var jsonResponse = await request.Content.ReadAsStringAsync();
                    var dto = JsonConvert.DeserializeObject<AsignacionDTO>(jsonResponse);
                    return [dto];
                }
                else
                {
                    return [];
                }
            }
            public async Task<IActionResult> InsertUpdate(AsignacionDTO elementDTO)
            {
                if (elementDTO.Id != 0)
                {
                    var httpClient = new HttpClient();
                    var response = await httpClient.PutAsJsonAsync($"{_configuration["Inven:URL"]}/Asignacion/{elementDTO.Id}", elementDTO);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Asignacion");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Asignacion");
                    }
                }
                else
                {
                    if (elementDTO.IdPersona <= 0 || elementDTO.IdEnsamble <= 0 )
                    {
                        Console.WriteLine("Al menos debe llenar un campo");
                        return RedirectToAction("Index", "Asignacion");
                    }
                    else
                    {
                        var client = new HttpClient();
                        var request = await client.PostAsJsonAsync($"{_configuration["Inven:URL"]}/Asignacion", elementDTO);
                        if (request.IsSuccessStatusCode)
                        {
                            //var re = await request.Content.ReadAsStringAsync();
                            return RedirectToAction("Index", "Asignacion"); ;
                        }
                        else
                        {
                            return RedirectToAction("Index", "Asignacion"); ;
                        }
                    }
                }

            }

            [HttpDelete]
            public async Task<bool> EliminarArea(string deleteid)
            {
                var client = new HttpClient();
                var response = await client.DeleteAsync($"{_configuration["Inven:URL"]}/Asignacion/{deleteid}");
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
            public async Task<List<PersonasDTO>> ListaPersonas()
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
            [HttpGet]
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
