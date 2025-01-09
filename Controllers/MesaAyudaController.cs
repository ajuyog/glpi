using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using noa.Models;
using System.Net.Sockets;


namespace noa.Controllers
{
    public class MesaAyudaController : Controller
    {
        private readonly IConfiguration _configuration;

        public MesaAyudaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(string buscaId)
        {
            var model = string.IsNullOrEmpty(buscaId) ? await ListaDTO() : await BuscaId(buscaId);
            return View("~/Views/MesaAyuda/Index.cshtml", model);
        }

        [HttpGet]
        public async Task<List<MesadeAyudaDTO>> ListaDTO()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{_configuration["Inven:Prueba"]}/Solicitud/Linq");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<MesadeAyudaDTO>>(jsonResponse) ?? new List<MesadeAyudaDTO>();
            }

            return new List<MesadeAyudaDTO>();
        }

        public async Task<List<MesadeAyudaDTO>> BuscaId(string buscaId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{_configuration["Inven:Prueba"]}/Solicitud/Linq/{buscaId}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<MesadeAyudaDTO>(jsonResponse);
                return dto != null ? new List<MesadeAyudaDTO> { dto } : new List<MesadeAyudaDTO>();
            }

            return new List<MesadeAyudaDTO>();
        }

        public IActionResult NuevoTicket()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NuevoTicket(MesadeAyudaDTO ticket, IFormFile captura)
        {
            if (ModelState.IsValid)
            {
                // Manejar el archivo adjunto
                if (captura != null && captura.Length > 0)
                {
                    var fileName = Path.GetFileName(captura.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await captura.CopyToAsync(stream);
                    }

                    // Guardar la ruta relativa del archivo
                    ticket.Captura = $"/Uploads/{fileName}";
                }

                // Asignar el usuario autenticado y la fecha de creación
                ticket.Usuario = User.Identity?.Name ?? "Anónimo";
                ticket.FechaCreacion = DateTime.Now;

                // Convertir el objeto a una cadena de consulta
                var queryParams = new Dictionary<string, string>
                    {
                        { "Usuario", ticket.Usuario },
                        { "Descripcion", ticket.Descripcion },
                        { "FechaCreacion", ticket.FechaCreacion.ToString("o") }, // Formato ISO 8601
                        { "Captura", ticket.Captura ?? "" }
                    };

                // Crear el query string
                var queryString = string.Join("&", queryParams.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));

                using var client = new HttpClient();
                var baseUrl = _configuration["Inven:Prueba"];
                var apiUrl = $"{baseUrl}/Solicitud/Linq?{queryString}";

                // Realizar la solicitud HTTP
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "El ticket se ha creado exitosamente.";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Error al guardar el ticket en el servidor.");
                }
            }

            return View(ticket);
        }

    }
}
