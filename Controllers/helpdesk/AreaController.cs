using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using noa.Models;
using System.Text;

public class AreaController : Controller
{
    private readonly ILogger<AreaController> _logger;
    private readonly IConfiguration _configuration;
    private new readonly string Url;


    public AreaController(ILogger<AreaController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        //Url = configuration["Inven:URL"];
        Url = _configuration["Inven:URL"];

    }

    [HttpGet]
    public IActionResult Index(string buscaid)
    {
        var model = string.IsNullOrEmpty(buscaid) ? ListaDTO().Result : BuscaId(buscaid).Result;
        return View("~/Views/helpdesk/area/Index.cshtml", model);
    }

    public async Task<List<AreaDTO>> ListaDTO()
    {
        var client = new HttpClient();
        //var request = new HttpRequestMessage(HttpMethod.Get, "{_configuration["HG:UR"]}/ActividadMovil");
        //var response = await client.SendAsync(request);
        var response = await client.GetAsync($"{Url}/Area");
        if (response.IsSuccessStatusCode)
        {
            var re = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<AreaDTO>>(re);
        }
        else
        {
            return [];
        }
    }

    public async Task<List<AreaDTO>> BuscaId(string buscaid)
    {
        var client = new HttpClient();
        var request = await client.GetAsync($"{Url}/{buscaid}");
        if (request.IsSuccessStatusCode)
        {
            var jsonResponse = await request.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<AreaDTO>(jsonResponse);
            return [dto];
        }
        else
        {
            return [];
        }
    }


    [HttpPost]
    public async Task<IActionResult> InsertUpdate(AreaDTO areaDTO)
    {
        var model = ListaDTO().Result;
        if (areaDTO.Id != 0)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.PutAsJsonAsync($"{Url}/{areaDTO.Id}", areaDTO);
            if (response.IsSuccessStatusCode)
            {
                //var responseData = await response.Content.ReadAsStringAsync();           
                return RedirectToAction("Index", "Area");
            }
            else
            {
                return RedirectToAction("Index", "Area");

            }
        }
        else
        {
            if (string.IsNullOrEmpty(areaDTO.Nombre))
            {
                Console.WriteLine("Al menos debe llenar un campo");
                return RedirectToAction("Index", "Area");


            }
            else
            {
                var client = new HttpClient();
                var request = await client.PostAsJsonAsync($"{Url}", areaDTO);
                if (request.IsSuccessStatusCode)
                {
                    //var re = await request.Content.ReadAsStringAsync();
                    return RedirectToAction("Index", "Area");


                }
                else
                {
                    return RedirectToAction("Index", "Area");


                }
            }
        }

    }

    [HttpDelete]
    public async Task<bool> EliminarArea(string deleteid)
    {
        var client = new HttpClient();
        var response = await client.DeleteAsync($"{Url}/{deleteid}");
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

