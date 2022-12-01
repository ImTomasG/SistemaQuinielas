using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaQuinielas.Models;
using System.Data.SqlClient;
using System.Data;

namespace SistemaQuinielas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AprobarSolicitudController : ControllerBase
    {
        private readonly string ConexionSQL;

        //Cadena de conexion a la base de datos
        public AprobarSolicitudController(IConfiguration config)
        {
            ConexionSQL = config.GetConnectionString("ConexionSQL");
        }



        [HttpGet]
        [Route("{respuesta}/{idUsuario}/{idLiga}")]
        public AprobarSolicitud AprobacionSolicitud(int respuesta, int idUsuario, int idLiga)
        {
            AprobarSolicitud oAprobarSolicitud = new AprobarSolicitud();
            using (var conexion = new SqlConnection(ConexionSQL))
            {
                var cmd = new SqlCommand("SP_APROBARSOLICITUD", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RESPUESTA", respuesta);
                cmd.Parameters.AddWithValue("@IDUSUARIO", idUsuario);
                cmd.Parameters.AddWithValue("@IDLIGA", idLiga);

                try
                {
                    conexion.Open();

                    using (SqlDataReader datosObtenidos = cmd.ExecuteReader())
                    {
                        while (datosObtenidos.Read())
                        {
                            oAprobarSolicitud = new AprobarSolicitud()
                            {
                                MENSAJE = datosObtenidos["MENSAJE"].ToString()
                            };
                        }
                    }
                    return oAprobarSolicitud;

                }
                catch (Exception ex)
                {
                    return oAprobarSolicitud;
                }

            }
        }


    }
}
