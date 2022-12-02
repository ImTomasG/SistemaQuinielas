using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaQuinielas.Models;
using System.Data.SqlClient;
using System.Data;

namespace SistemaQuinielas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrearLigaController : ControllerBase
    {
        private readonly string ConexionSQL;

        //Cadena de conexion a la base de datos
        public CrearLigaController(IConfiguration config)
        {
            ConexionSQL = config.GetConnectionString("ConexionSQL");
        }



        [HttpGet]
        [Route("{idUsuario}/{nombreLiga}/{tipoLiga}/{precio}/{idRol}")]
        public CrearLiga CreacionLiga(int idUsuario,string nombreLiga,string tipoLiga,decimal precio,int idRol)
        {
            CrearLiga oCrearLiga = new CrearLiga();
            using (var conexion = new SqlConnection(ConexionSQL))
            {
                var cmd = new SqlCommand("SP_CREARLIGA", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_USUARIO", idUsuario);
                cmd.Parameters.AddWithValue("@NOMBRE_LIGA", nombreLiga);
                cmd.Parameters.AddWithValue("@TIPO_LIGA", tipoLiga);
                cmd.Parameters.AddWithValue("@PRECIO", precio);
                cmd.Parameters.AddWithValue("@ID_ROL", idRol);

                try
                {
                    conexion.Open();

                    using (SqlDataReader datosObtenidos = cmd.ExecuteReader())
                    {
                        while (datosObtenidos.Read())
                        {
                            oCrearLiga = new CrearLiga()
                            {
                                MENSAJE = datosObtenidos["MENSAJE"].ToString()
                            };


                        }
                    }
                    return oCrearLiga;

                }
                catch (Exception ex)
                {
                    return oCrearLiga;
                }

            }
        }



    }
}
