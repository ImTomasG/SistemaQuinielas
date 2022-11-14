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
    public class RegistroController : ControllerBase
    {
        private readonly string ConexionSQL;

        //Cadena de conexion a la base de datos
        public RegistroController(IConfiguration config)
        {
            ConexionSQL = config.GetConnectionString("ConexionSQL");
        }


        [HttpPost]
        [Route("NuevoUsuario")]

        public IActionResult Registro([FromForm] Registro datos)
        {
            Registro oRegistro = new Registro();

            try
            {
                using (var conexion = new SqlConnection(ConexionSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("SP_REGISTRO", conexion);
                    cmd.Parameters.AddWithValue("NOMBRES", datos.NOMBRES);
                    cmd.Parameters.AddWithValue("APELLIDOS", datos.APELLIDOS);
                    cmd.Parameters.AddWithValue("TELEFONO", datos.TELEFONO);
                    cmd.Parameters.AddWithValue("EMAIL", datos.EMAIL);
                    cmd.Parameters.AddWithValue("PASSWORD", datos.PASSWORD);
                    cmd.CommandType = CommandType.StoredProcedure;

                    
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (dr["MENSAJE_ERROR"].ToString().Length > 0)
                            {
                                oRegistro = new Registro()
                                {
                                    MENSAJE_ERROR = dr["MENSAJE_ERROR"].ToString()
                                };

                            }
                        }
                    }
                    

                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Conexión Establecida", response = oRegistro });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new {mensaje = error.Message});
            }
        }


    }
}
