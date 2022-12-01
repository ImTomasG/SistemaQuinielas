using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaQuinielas.Models;
using System.Data.SqlClient;
using System.Data;

namespace SistemaQuinielas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApuestaController : ControllerBase
    {
        private readonly string ConexionSQL;

        //Cadena de conexion a la base de datos
        public ApuestaController(IConfiguration config)
        {
            ConexionSQL = config.GetConnectionString("ConexionSQL");
        }



        [HttpGet]
        [Route("{idJornada}/{idUsuario}/{idLiga}/{idLocal}/{idVisitante}/{apuestaLocal}/{apuestaVisitante}")]
        public Apuesta ApuestaPartidos(string idJornada,int idUsuario,int idLiga,string idLocal,string idVisitante,int apuestaLocal,int apuestaVisitante)
        {
            Apuesta oApuesta = new Apuesta();
            using (var conexion = new SqlConnection(ConexionSQL))
            {
                var cmd = new SqlCommand("SP_APOSTAR", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDJORNADA", idJornada);
                cmd.Parameters.AddWithValue("@IDUSUARIO", idUsuario);
                cmd.Parameters.AddWithValue("@IDLIGA", idLiga);
                cmd.Parameters.AddWithValue("@IDLOCAL", idLocal);
                cmd.Parameters.AddWithValue("@IDVISITANTE", idVisitante);
                cmd.Parameters.AddWithValue("@APUESTALOCAL", apuestaLocal);
                cmd.Parameters.AddWithValue("@APUESTAVISITANTE", apuestaVisitante);

                try
                {
                    conexion.Open();

                    using (SqlDataReader datosObtenidos = cmd.ExecuteReader())
                    {
                        while (datosObtenidos.Read())
                        {
                            oApuesta = new Apuesta()
                            {
                                MENSAJE = datosObtenidos["MENSAJE"].ToString()
                            };

                            


                        }
                    }
                    return oApuesta;

                }
                catch (Exception ex)
                {
                    return oApuesta;
                }

            }
        }



    }
}
