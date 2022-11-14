using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using SistemaQuinielas.Models;
using System.Data;
using System.Data.SqlClient;

namespace SistemaQuinielas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActualizarResultadosController : ControllerBase
    {
        private readonly string ConexionSQL;

        //Cadena de conexion a la base de datos
        public ActualizarResultadosController(IConfiguration config)
        {
            ConexionSQL = config.GetConnectionString("ConexionSQL");
        }

        [HttpGet]
        [Route("{id_jornada}/{id_equipolocal}/{id_equipovisitante}/{goles_local:int}/{goles_visitante:int}")]
        public IActionResult ActualizarGoles(string id_jornada,string id_equipolocal, string id_equipovisitante,int goles_local,int goles_visitante)
        {
            ActualizarResultados oResultado = new ActualizarResultados();
            using (var conexion = new SqlConnection(ConexionSQL))
            {
                var cmd = new SqlCommand("SP_ACTUALIZACION_RESULTADOS", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_JORNADA", id_jornada);
                cmd.Parameters.AddWithValue("@ID_EQUIPOLOCAL", id_equipolocal);
                cmd.Parameters.AddWithValue("@ID_EQUIPOVISITANTE", id_equipovisitante);
                cmd.Parameters.AddWithValue("@GOLES_LOCAL", goles_local);
                cmd.Parameters.AddWithValue("@GOLES_VISITANTE", goles_visitante);

                try
                {
                    conexion.Open();

                    using (SqlDataReader datosObtenidos = cmd.ExecuteReader())
                    {
                        while (datosObtenidos.Read())
                        {
                            if (datosObtenidos["MENSAJE_ERROR"].ToString().Length > 0)
                            {
                                oResultado = new ActualizarResultados()
                                {
                                    MENSAJE_ERROR = datosObtenidos["MENSAJE_ERROR"].ToString()
                                };
                            }
                        }
                    }
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "Conexión Establecida", response = oResultado });
                }
                catch (Exception error)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
                }

            }
        }


    }
}
