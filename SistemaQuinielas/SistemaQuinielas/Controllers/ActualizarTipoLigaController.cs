using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaQuinielas.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Win32;

namespace SistemaQuinielas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActualizarTipoLigaController : ControllerBase
    {
        private readonly string ConexionSQL;

        //Cadena de conexion a la base de datos
        public ActualizarTipoLigaController(IConfiguration config)
        {
            ConexionSQL = config.GetConnectionString("ConexionSQL");
        }



        [HttpGet]
        [Route("{idLiga}/{idTipoLiga}")]
        public ActualizarTipoLiga ActualizarLiga(int idLiga, int idTipoLiga)
        {
            ActualizarTipoLiga oActualizarTipoLiga = new ActualizarTipoLiga();
            using (var conexion = new SqlConnection(ConexionSQL))
            {
                var cmd = new SqlCommand("SP_TLIGA", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_LIGAS", idLiga);
                cmd.Parameters.AddWithValue("@ID_TIPOLIGAS", idTipoLiga);

                try
                {
                    conexion.Open();

                    using (SqlDataReader datosObtenidos = cmd.ExecuteReader())
                    {
                        while (datosObtenidos.Read())
                        {
                            if (datosObtenidos["MENSAJE_ERROR"].ToString().Length < 1)
                            {
                                oActualizarTipoLiga = new ActualizarTipoLiga()
                                {
                                    
                                    ID_LIGA = Convert.ToInt32(datosObtenidos["ID_LIGA"]),
                                    PRECIO = Convert.ToDecimal(datosObtenidos["PRECIO"]),
                                    NOMBRE_LIGA = datosObtenidos["NOMBRE_LIGA"].ToString(),
                                    ID_TIPOLIGA = Convert.ToInt32(datosObtenidos["ID_TIPOLIGA"]),
                                    NOMBRE_TIPOLIGA = datosObtenidos["NOMBRE_TIPOLIGA"].ToString()
                                };

                            }
                            else
                            {
                                oActualizarTipoLiga = new ActualizarTipoLiga()
                                {

                                    MENSAJE_ERROR = datosObtenidos["MENSAJE_ERROR"].ToString()
                                };
                            }
                            
                        }
                    }
                    return oActualizarTipoLiga;

                }
                catch (Exception ex)
                {
                    return oActualizarTipoLiga;
                }

            }
        }





    }
}
