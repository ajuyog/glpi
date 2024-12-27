using System.Reflection.Metadata;

namespace noa.Models
{
    public class MesadeAyudaDTO
    {
        public int Id { get; set; }
        public string Categoria { get; set; }
        public string Error { get; set; }
        public string Descripccion { get; set; }
        public Byte[] Imagen { get; set; }
    }
}
