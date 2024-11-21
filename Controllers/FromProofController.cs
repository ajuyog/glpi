using Microsoft.AspNetCore.Mvc;
using noa.Models;
using Newtonsoft.Json;


public class FromProofController : Controller
{
    private readonly ILogger<FromProofController> _logger;
    private readonly IConfiguration _configuration;

    public FromProofController(ILogger<FromProofController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public IActionResult Index(string buscaid)
    {
        var model = string.IsNullOrEmpty(buscaid) ? ListaDTO().Result : BuscaId(buscaid).Result;
        return View("~/Views/FromProof/Index.cshtml", model);
    }

    public async Task<List<prueba3DTO>> ListaDTO()
    {
        var client = new HttpClient();
        //var request = new HttpRequestMessage(HttpMethod.Get, "{_configuration["HG:UR"]}/ActividadMovil");
        //var response = await client.SendAsync(request);
        var response = await client.GetAsync($"{_configuration["HG:UR"]}/ActividadMovil");
        if (response.IsSuccessStatusCode)
        {
            var re = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<prueba3DTO>>(re);
        }
        else
        {
            return [];
        }
    }

    [HttpPost]
    public async Task<IActionResult> InsertDTO(prueba3DTO prueba3DTO)
    {
        if (string.IsNullOrEmpty(prueba3DTO.codSecundario1) && string.IsNullOrEmpty(prueba3DTO.codSecundario2) &&
            string.IsNullOrEmpty(prueba3DTO.codSecundario3) && string.IsNullOrEmpty(prueba3DTO.codSecundario4) &&
            string.IsNullOrEmpty(prueba3DTO.codSecundario5) && string.IsNullOrEmpty(prueba3DTO.codSecundario6) &&
            string.IsNullOrEmpty(prueba3DTO.codSecundario7) && string.IsNullOrEmpty(prueba3DTO.codSecundario8) &&
            string.IsNullOrEmpty(prueba3DTO.codSecundario9) && string.IsNullOrEmpty(prueba3DTO.codSecundario10) &&
            string.IsNullOrEmpty(prueba3DTO.codSecundario11) && string.IsNullOrEmpty(prueba3DTO.codSecundario12) &&
            string.IsNullOrEmpty(prueba3DTO.codSecundario13) && string.IsNullOrEmpty(prueba3DTO.codSecundario14) &&
            string.IsNullOrEmpty(prueba3DTO.codSecundario15) && string.IsNullOrEmpty(prueba3DTO.codSecundario16))
        {
            Console.WriteLine("Al menos debe llenar un campo");
            var model = ListaDTO().Result;
            return View("~/Views/FromProof/Index.cshtml", model);
        }
        else
        {
            var client = new HttpClient();
            var request = await client.PostAsJsonAsync($"{_configuration["HG:UR"]}/ActividadMovil", prueba3DTO);
            if (request.IsSuccessStatusCode)
            {
                var re = await request.Content.ReadAsStringAsync();
                var model = ListaDTO().Result;
                return View("~/Views/FromProof/Index.cshtml", model);
            }
            else
            {
                var model = ListaDTO().Result;
                return View("~/Views/FromProof/Index.cshtml", model);
            }
        }
    }

    public async Task<List<prueba3DTO>> BuscaId(string buscaid)
    {
        var client = new HttpClient();
        var request = await client.GetAsync($"{_configuration["HG:UR"]}/ActividadMovil/{buscaid}");
        if (request.IsSuccessStatusCode)
        {
            var jsonResponse = await request.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<prueba3DTO>(jsonResponse);
            return [dto];
        }
        else
        {
            return [];
        }
    }

    [HttpGet]
    public async Task<prueba3DTO> obtedellausua(int id)
    {
        var _httpClient = new HttpClient();
        var response = await _httpClient.GetAsync($"{_configuration["HG:UR"]}/ActividadMovil/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<prueba3DTO>(content);
        }
        else
        {
            return new prueba3DTO();
        }
    }

}


