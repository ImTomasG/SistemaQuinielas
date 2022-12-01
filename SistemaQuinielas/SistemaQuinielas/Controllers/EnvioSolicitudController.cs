using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaQuinielas.Models;
using System.Data.SqlClient;
using System.Data;

namespace SistemaQuinielas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvioSolicitudController : ControllerBase
    {

        private readonly string ConexionSQL;

        //Cadena de conexion a la base de datos
        public EnvioSolicitudController(IConfiguration config)
        {
            ConexionSQL = config.GetConnectionString("ConexionSQL");
        }



        [HttpGet]
        [Route("{idUsuario}/{nombreLiga}")]
        public EnvioSolicitud EnviosSolicitud(string idUsuario, string nombreLiga)
        {
            EnvioSolicitud oEnvioSolicitud = new EnvioSolicitud();
            using (var conexion = new SqlConnection(ConexionSQL))
            {
                var cmd = new SqlCommand("SP_ENVIOSOLICITUD", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDUSUARIO", idUsuario);
                cmd.Parameters.AddWithValue("@NOMLIGA", nombreLiga);

                try
                {
                    conexion.Open();

                    using (SqlDataReader datosObtenidos = cmd.ExecuteReader())
                    {
                        while (datosObtenidos.Read())
                        {
                            oEnvioSolicitud = new EnvioSolicitud()
                            {
                                MENSAJE = datosObtenidos["MENSAJE"].ToString()
                            };


                        }
                    }
                    return oEnvioSolicitud;

                }
                catch (Exception ex)
                {
                    return oEnvioSolicitud;
                }

            }
        }



    }
}
