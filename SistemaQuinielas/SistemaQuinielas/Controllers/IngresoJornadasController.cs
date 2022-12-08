using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaQuinielas.Models;
using System.Data.SqlClient;
using System.Data;

namespace SistemaQuinielas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngresoJornadasController : ControllerBase
    {
        private readonly string ConexionSQL;

        //Cadena de conexion a la base de datos
        public IngresoJornadasController(IConfiguration config)
        {
            ConexionSQL = config.GetConnectionString("ConexionSQL");
        }

        [HttpGet]
        [Route("{idJornada}/{nombreJornada}")]
        public IngresoJornadas IngresoJornada(string idJornada, string nombreJornada)
        {
            IngresoJornadas oIngresoJornadas = new IngresoJornadas();
            using (var conexion = new SqlConnection(ConexionSQL))
            {
                var cmd = new SqlCommand("SP_INGRESOJORNADAS", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_JORNADA", idJornada);
                cmd.Parameters.AddWithValue("@NOMBRE_JORNADA", nombreJornada);

                try
                {
                    conexion.Open();

                    using (SqlDataReader datosObtenidos = cmd.ExecuteReader())
                    {
                        while (datosObtenidos.Read())
                        {
                            oIngresoJornadas = new IngresoJornadas()
                            {
                                MENSAJE = datosObtenidos["MENSAJE"].ToString()
                            };


                        }
                    }
                    return oIngresoJornadas;

                }
                catch (Exception)
                {
                    return oIngresoJornadas;
                }

            }
        }





    }
}
