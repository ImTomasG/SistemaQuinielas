namespace SistemaQuinielas.Models
{
    public class Apuesta
    {
        public int ID_JORNADA { get; set; }
        public decimal ID_USUARIO { get; set; }
        public string ID_LIGA { get; set; }
        public int ID_LOCAL { get; set; }
        public string ID_VISITANTE { get; set; }
        public string APUESTA_LOCAL { get; set; }
        public string APUESTA_VISITANTE { get; set; }
        public string MENSAJE { get; set; }
    }
}
