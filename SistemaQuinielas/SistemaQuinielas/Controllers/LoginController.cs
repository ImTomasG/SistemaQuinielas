using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaQuinielas.Models;
using System.Data;
using System.Data.SqlClient;


namespace SistemaQuinielas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly string ConexionSQL;

        //Cadena de conexion a la base de datos
        public LoginController(IConfiguration config)
        {
            ConexionSQL = config.GetConnectionString("ConexionSQL");
        }

        [HttpGet]
        [Route("{email}/{password}")]
        public Login Login(string email, string password)
        {
            Login oLogin = new Login();
            using (var conexion = new SqlConnection(ConexionSQL))
            {
                var cmd = new SqlCommand("SP_CREDENCIALES", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EMAIL", email);
                cmd.Parameters.AddWithValue("@PASSWORD", password);

                try
                {
                    conexion.Open();

                    using (SqlDataReader datosObtenidos = cmd.ExecuteReader())
                    {
                        while (datosObtenidos.Read())
                        {
                            oLogin = new Login()
                            {
                                ID_USUARIO = Convert.ToInt32(datosObtenidos["ID_USUARIO"]),
                                NOMBRES = datosObtenidos["NOMBRES"].ToString(),
                                APELLIDOS = datosObtenidos["APELLIDOS"].ToString(),
                                TELEFONO = Convert.ToInt32(datosObtenidos["TELEFONO"]),
                                CORREO = datosObtenidos["CORREO"].ToString()
                            };
                        }
                    }
                    return oLogin;

                }
                catch (Exception ex)
                {
                    return oLogin;
                }

            }
        }

    }
}
