using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaQuinielas.Models;
using System.Data.SqlClient;
using System.Data;

namespace SistemaQuinielas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvitacionesCorreoController : ControllerBase
    {

        private readonly string ConexionSQL;

        //Cadena de conexion a la base de datos
        public InvitacionesCorreoController(IConfiguration config)
        {
            ConexionSQL = config.GetConnectionString("ConexionSQL");
        }



        [HttpGet]
        [Route("{idLiga}/{email}")]
        public InvitacionesCorreo InvitacionCorreo(string idLiga, string email)
        {
            InvitacionesCorreo oInvitacionesCorreo = new InvitacionesCorreo();
            using (var conexion = new SqlConnection(ConexionSQL))
            {
                var cmd = new SqlCommand("SP_INVITACIONEMAIL", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_LIGA", idLiga);
                cmd.Parameters.AddWithValue("@EMAIL", email);

                try
                {
                    conexion.Open();

                    using (SqlDataReader datosObtenidos = cmd.ExecuteReader())
                    {
                        while (datosObtenidos.Read())
                        {
                            oInvitacionesCorreo = new InvitacionesCorreo()
                            {
                                MENSAJE = datosObtenidos["MENSAJE"].ToString()
                            };


                        }
                    }
                    return oInvitacionesCorreo;

                }
                catch (Exception ex)
                {
                    return oInvitacionesCorreo;
                }

            }
        }


    }
}
