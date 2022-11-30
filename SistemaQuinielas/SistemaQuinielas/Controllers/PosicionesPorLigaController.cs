using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaQuinielas.Models;
using System.Data.SqlClient;
using System.Data;

namespace SistemaQuinielas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PosicionesPorLigaController : ControllerBase
    {
        private readonly string ConexionSQL;
        //Cadena de conexion a la base de datos
        public PosicionesPorLigaController(IConfiguration config)
        {
            ConexionSQL = config.GetConnectionString("ConexionSQL");
        }


        [HttpGet]
        [Route("{idLiga}")]
        public List<PosicionesPorLiga> PosicionLiga(int idLiga)
        {
            List<PosicionesPorLiga> DatosPosiciones = new List<PosicionesPorLiga>();
            using (var conexion = new SqlConnection(ConexionSQL))
            {
                var cmd = new SqlCommand("SP_POSICION_LIGA", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_LIGA", idLiga);


                try
                {
                    conexion.Open();

                    using (SqlDataReader datosObtenidos = cmd.ExecuteReader())
                    {
                        while (datosObtenidos.Read())
                        {
                            DatosPosiciones.Add(new PosicionesPorLiga()
                            {
                                ID_LIGA = Convert.ToInt32(datosObtenidos["ID_LIGA"]),
                                NOMBRE_LIGA = datosObtenidos["NOMBRE_LIGA"].ToString(),
                                ID_USUARIO = Convert.ToInt32(datosObtenidos["ID_USUARIO"]),
                                NOMBRES = datosObtenidos["NOMBRES"].ToString(),
                                APELLIDOS = datosObtenidos["APELLIDOS"].ToString(),
                                ACUMULACION_PUNTOS = Convert.ToInt32(datosObtenidos["ACUMULACION_PUNTOS"])
                            });
                        }
                    }
                    return DatosPosiciones;

                }
                catch (Exception ex)
                {
                    return DatosPosiciones;
                }

            }
        }



    }
}
