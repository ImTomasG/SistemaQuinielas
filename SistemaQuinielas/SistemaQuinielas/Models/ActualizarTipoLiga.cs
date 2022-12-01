namespace SistemaQuinielas.Models
{
    public class ActualizarTipoLiga
    {
        public int ID_LIGA { get; set; }
        public decimal PRECIO { get; set; }
        public string NOMBRE_LIGA { get; set; }
        public int ID_TIPOLIGA { get; set; }
        public string NOMBRE_TIPOLIGA { get; set; }
        public string MENSAJE_ERROR { get; set; }
    }
}

