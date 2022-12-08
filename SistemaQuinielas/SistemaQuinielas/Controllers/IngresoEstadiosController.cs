using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaQuinielas.Models;
using System.Data.SqlClient;
using System.Data;

namespace SistemaQuinielas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngresoEstadiosController : ControllerBase
    {
        private readonly string ConexionSQL;

        //Cadena de conexion a la base de datos
        public IngresoEstadiosController(IConfiguration config)
        {
            ConexionSQL = config.GetConnectionString("ConexionSQL");
        }

        [HttpGet]
        [Route("{idEstadio}/{nombreEstadio}")]
        public IngresoEstadios IngresoEstadios(string idEstadio, string nombreEstadio)
        {
            IngresoEstadios oIngresoEstadios = new IngresoEstadios();
            using (var conexion = new SqlConnection(ConexionSQL))
            {
                var cmd = new SqlCommand("SP_INGRESOESTADIOS", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_ESTADIO", idEstadio);
                cmd.Parameters.AddWithValue("@NOMBRE_ESTADIO", nombreEstadio);

                try
                {
                    conexion.Open();

                    using (SqlDataReader datosObtenidos = cmd.ExecuteReader())
                    {
                        while (datosObtenidos.Read())
                        {
                            oIngresoEstadios = new IngresoEstadios()
                            {
                                MENSAJE = datosObtenidos["MENSAJE"].ToString()
                            };


                        }
                    }
                    return oIngresoEstadios;

                }
                catch (Exception)
                {
                    return oIngresoEstadios;
                }

            }
        }






    }
}
