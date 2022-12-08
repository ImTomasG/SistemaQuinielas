using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaQuinielas.Models;
using System.Data.SqlClient;
using System.Data;

namespace SistemaQuinielas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngresoPartidosController : ControllerBase
    {
        private readonly string ConexionSQL;

        //Cadena de conexion a la base de datos
        public IngresoPartidosController(IConfiguration config)
        {
            ConexionSQL = config.GetConnectionString("ConexionSQL");
        }


        [HttpGet]
        [Route("{idJornada}/{idEuipoLocal}/{idEquipoVisitante}/{idEstadio}/{fecha}")]
        public IngresoPartidos IngresoPartido(string idJornada,string idEuipoLocal,string idEquipoVisitante,string idEstadio,string fecha)
        {
            IngresoPartidos oIngresoPartidos = new IngresoPartidos();
            using (var conexion = new SqlConnection(ConexionSQL))
            {
                var cmd = new SqlCommand("SP_INGRESOPARTIDOS", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_JORNADA", idJornada);
                cmd.Parameters.AddWithValue("@ID_EQUIPOLOCAL", idEuipoLocal);
                cmd.Parameters.AddWithValue("@ID_EQUIPOVISITANTE", idEquipoVisitante);
                cmd.Parameters.AddWithValue("@ID_ESTADIO", idEstadio);
                cmd.Parameters.AddWithValue("@FECHA", fecha);

                try
                {
                    conexion.Open();

                    using (SqlDataReader datosObtenidos = cmd.ExecuteReader())
                    {
                        while (datosObtenidos.Read())
                        {
                            oIngresoPartidos = new IngresoPartidos()
                            {
                                MENSAJE = datosObtenidos["MENSAJE"].ToString()
                            };


                        }
                    }
                    return oIngresoPartidos;

                }
                catch (Exception)
                {
                    return oIngresoPartidos;
                }

            }
        }




    }
}
