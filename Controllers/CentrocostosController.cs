using Microsoft.AspNetCore.Mvc;

namespace noa.Controllers
{
    public class CentrocostosController : Controller
    {
        private readonly IConfiguration _configuration;
        public CentrocostosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View("~/Views/Centrocostos/Index.cshtml");
        }
    }

//    public async Task<List<CentrodecostosDTO>> datoscostos()
//    {
//        var client = new HttpClient();
//        var response = await client.GetAsync($"{_configuration["Inven:URL"]}/centrocostos");
//        if (response.IsSuccessStatusCode)
//        {
//            var Jsonresponse = await response.Content.ReadAsStringAsync();
//            return JsonConvert.DeserializeObject<List<CentrocostosDTO>>(Jsonresponse);
//        }
//        else
//        {
//            return [];
//        }
//    }
}





