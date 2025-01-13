using System.Reflection.Metadata;

namespace noa.Models
{
    public class MesadeAyudaDTO
    {
        public int Id { get; set; }
        public string Nombreapellidos { get; set; }
        public string Usuario { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public string Solicitud { get; set; }
        public string Area { get; set; }
        public string Error { get; set; }
        public string EstatusId { get; set; }
        public string EstadoId { get; set; }
        public DateTime FechaCreacion { get; set; }

    }
}
