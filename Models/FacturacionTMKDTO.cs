﻿namespace noa.Models
{
    public class FacturacionTMKDTO
    {
        public int Id { get; set; }
        public float VlrNeto { get; set; }
        public int IdEnsamble { get; set; }
        public string Descripcion { get; set; }
        public DateOnly? Fecha { get; set; }
    }
}
