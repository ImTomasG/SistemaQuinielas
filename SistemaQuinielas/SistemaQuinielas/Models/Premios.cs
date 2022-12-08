namespace SistemaQuinielas.Models
{
    public class Premios
    {
        public int ID_LIGA { get; set; }
        public string NOMBRE_LIGA { get; set; }
        public int ID_USUARIO { get; set; }
        public string NOMBRES { get; set; }
        public string APELLIDOS { get; set; }
        public int ACUMULACION_PUNTOS { get; set; }
        public int POSICIONFILAS { get; set; }
        public decimal PREMIO_CONSEGUIDO { get; set; }
    }
}
