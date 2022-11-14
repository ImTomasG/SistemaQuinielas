namespace SistemaQuinielas.Models
{
    public class ActualizarResultados
    {
        public string ID_JORNADA { get; set; }
        public string ID_EQUIPOLOCAL { get; set; }
        public string ID_EQUIPOVISITANTE { get; set; }
        public int GOLES_LOCAL { get; set; }
        public int GOLES_VISITANTE { get; set; }
        public string MENSAJE_ERROR { get; set; }
    }
}
