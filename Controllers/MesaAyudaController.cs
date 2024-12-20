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

         public IActionResult Index()
         {
             return View("~/Views/MesaAyuda/Index.cshtml");
         }
    }
         
}
