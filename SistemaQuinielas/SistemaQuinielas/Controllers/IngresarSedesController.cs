using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaQuinielas.Models;
using System.Data.SqlClient;
using System.Data;

namespace SistemaQuinielas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngresarSedesController : ControllerBase
    {
        private readonly string ConexionSQL;

        //Cadena de conexion a la base de datos
        public IngresarSedesController(IConfiguration config)
        {
            ConexionSQL = config.GetConnectionString("ConexionSQL");
        }



        [HttpGet]
        [Route("{idEstadio}/{nombreEstadio}")]
        public IngresarSedes IngresoSedes(string idEstadio,string nombreEstadio)
        {
            IngresarSedes oIngresarSedes = new IngresarSedes();
            using (var conexion = new SqlConnection(ConexionSQL))
            {
                var cmd = new SqlCommand("SP_INGRESAR_SEDE", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDESTADIO", idEstadio);
                cmd.Parameters.AddWithValue("@NOMBREESTADIO", nombreEstadio);

                try
                {
                    conexion.Open();

                    using (SqlDataReader datosObtenidos = cmd.ExecuteReader())
                    {
                        while (datosObtenidos.Read())
                        {
                            oIngresarSedes = new IngresarSedes()
                            {
                                ID_ESTADIO = datosObtenidos["ID_ESTADIO"].ToString(),
                                NOMBRE_ESTADIO = datosObtenidos["NOMBRE_ESTADIO"].ToString(),
                                MENSAJE = datosObtenidos["MENSAJE"].ToString()
                            };




                        }
                    }
                    return oIngresarSedes;

                }
                catch (Exception ex)
                {
                    return oIngresarSedes;
                }

            }
        }







    }
}
