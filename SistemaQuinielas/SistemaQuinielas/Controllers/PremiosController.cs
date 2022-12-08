using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaQuinielas.Models;
using System.Data.SqlClient;
using System.Data;

namespace SistemaQuinielas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PremiosController : ControllerBase
    {
        private readonly string ConexionSQL;

        //Cadena de conexion a la base de datos
        public PremiosController(IConfiguration config)
        {
            ConexionSQL = config.GetConnectionString("ConexionSQL");
        }


        [HttpGet]
        [Route("{idLiga}")]
        public List<Premios> PremiosLiga(int idLiga)
        {
            List<Premios> oPremios = new List<Premios>();
            using (var conexion = new SqlConnection(ConexionSQL))
            {
                var cmd = new SqlCommand("SP_PREMIOSFINALES", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_LIGA", idLiga);


                try
                {
                    conexion.Open();

                    using (SqlDataReader datosObtenidos = cmd.ExecuteReader())
                    {
                        while (datosObtenidos.Read())
                        {
                            oPremios.Add(new Premios()
                            {
                                ID_LIGA = Convert.ToInt32(datosObtenidos["ID_LIGA"]),
                                NOMBRE_LIGA = datosObtenidos["NOMBRE_LIGA"].ToString(),
                                ID_USUARIO = Convert.ToInt32(datosObtenidos["ID_USUARIO"]),
                                NOMBRES = datosObtenidos["NOMBRES"].ToString(),
                                APELLIDOS = datosObtenidos["APELLIDOS"].ToString(),
                                ACUMULACION_PUNTOS = Convert.ToInt32(datosObtenidos["ACUMULACION_PUNTOS"]),
                                POSICIONFILAS = Convert.ToInt32(datosObtenidos["POSICIONFILAS"]),
                                PREMIO_CONSEGUIDO = Convert.ToDecimal(datosObtenidos["PREMIO_CONSEGUIDO"])
                            });


                        }
                    }
                    return oPremios;

                }
                catch (Exception)
                {
                    return oPremios;
                }

            }
        }





    }
}
