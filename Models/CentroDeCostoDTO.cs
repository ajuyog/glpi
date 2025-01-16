namespace noa.Models
{
    public class CentroCosto
    {
        public List<CentroDeCostoDTO>  EquiposSolos { get; set; }
        public List<AreaTotal> ValorPorArea { get; set; }
        public List<TotalGeneral> TotalGeneral { get; set; }
        //public areaTotal TotalGeneral { get; set; }
    }

    public class CentroDeCostoDTO
    {
      public int Id { get; set; }
      public int Marca { get; set; }
      public string NumeroSerial { get; set; }
      public bool Renting { get; set; }
      public string Area { get; set; }
      public int Empresa { get; set; }
      public float VlrNeto { get; set; }
      public float Iva19 { get; set; }
      public string Factura { get; set; }


        //public int Id { get; set; }
        //public float Iva19 { get; set; }
        //public string Descripcion { get; set; }
        //public string NumeroSerial { get; set; }
        //public string IdEmpresa { get; set; }
        //public bool Renting { get; set; }
        //public int Total { get; set; }
        //public string TipoDeElemento { get; set; }
        //public string Marca { get; set; }



        //public string Factura { get; set; }
        //public float VlrNeto { get; set; }
        //public string NombreArea { get; set; }
        //public int TotalEquipos { get; set; }
        //public DateOnly Fecha { get; set; }
    }

    public class AreaTotal
    {
        public string Factura { get; set; }
        public float AreaVlrNeto { get; set; }
        public string NombreArea { get; set; }
        public int TotalEquipos { get; set; }
        public DateTime? Fecha { get; set; }
    }
    public class TotalGeneral
    {
        public float TotalVlrNeto { get; set; }
    }

}
