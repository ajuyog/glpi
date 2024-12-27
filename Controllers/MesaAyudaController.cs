using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using noa.Models;

namespace noa.Controllers
{
    public class MesaAyudaController : Controller
    {
        private readonly IConfiguration _configuration;
         public MesaAyudaController(IConfiguration configuration)
         {
             _configuration = configuration;
         }

        public IActionResult Index(/*string buscaid*/)
        {
            //var model = string.IsNullOrEmpty(buscaid) ? ListaDTO().Result : BuscaId(buscaid).Result;
            return View("~/Views/MesaAyuda/Index.cshtml");
        }
        //[HttpGet]
        //public async Task<List<MesadeAyudaDTO>> ListaDTO()
        //{
        //    var client = new HttpClient();

        //    // Realiza la solicitud GET
        //    var response = await client.GetAsync($"{_configuration["Inven:Prueba"]}/PruebaSolicitud");
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var jsonResponse = await response.Content.ReadAsStringAsync();
        //        return JsonConvert.DeserializeObject<List<MesadeAyudaDTO>>(jsonResponse);
        //    }
        //    else
        //    {
        //        return [];
        //    }
        //}
        //public async Task<List<MesadeAyudaDTO>> BuscaId(string buscaid)
        //{
        //    var client = new HttpClient();
        //    var request = await client.GetAsync($"{_configuration["Inven:Prueba"]}/PruebaSolicitud/{buscaid}");
        //    if (request.IsSuccessStatusCode)
        //    {
        //        var jsonResponse = await request.Content.ReadAsStringAsync();
        //        var dto = JsonConvert.DeserializeObject<MesadeAyudaDTO>(jsonResponse);
        //        return [dto];
        //    }
        //    else
        //    {
        //        return [];
        //    }
        //}
    }
    
}
