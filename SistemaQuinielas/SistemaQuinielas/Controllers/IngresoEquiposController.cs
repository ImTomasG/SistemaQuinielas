using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaQuinielas.Models;
using System.Data.SqlClient;
using System.Data;

namespace SistemaQuinielas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngresoEquiposController : ControllerBase
    {

        private readonly string ConexionSQL;

        //Cadena de conexion a la base de datos
        public IngresoEquiposController(IConfiguration config)
        {
            ConexionSQL = config.GetConnectionString("ConexionSQL");
        }



        [HttpGet]
        [Route("{idEquipo}/{nombreEquipo}/{idGrupo}")]
        public IngresoEquipos IngresoSedes(string idEquipo, string nombreEquipo, string idGrupo)
        {
            IngresoEquipos oIngresoEquipos = new IngresoEquipos();
            using (var conexion = new SqlConnection(ConexionSQL))
            {
                var cmd = new SqlCommand("SP_INGRESAR_TEAM", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDEQUIPO", idEquipo);
                cmd.Parameters.AddWithValue("@NOMBREEQUIPO", nombreEquipo);
                cmd.Parameters.AddWithValue("@IDGRUPO", idGrupo);

                try
                {
                    conexion.Open();

                    using (SqlDataReader datosObtenidos = cmd.ExecuteReader())
                    {
                        while (datosObtenidos.Read())
                        {
                            oIngresoEquipos = new IngresoEquipos()
                            {
                                MENSAJE = datosObtenidos["MENSAJE"].ToString()
                            };


                        }
                    }
                    return oIngresoEquipos;

                }
                catch (Exception ex)
                {
                    return oIngresoEquipos;
                }

            }
        }






    }
}
