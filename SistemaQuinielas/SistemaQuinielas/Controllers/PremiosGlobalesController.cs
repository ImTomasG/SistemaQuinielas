using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaQuinielas.Models;
using System.Data.SqlClient;
using System.Data;

namespace SistemaQuinielas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PremiosGlobalesController : ControllerBase
    {
        private readonly string ConexionSQL;
        //Cadena de conexion a la base de datos
        public PremiosGlobalesController(IConfiguration config)
        {
            ConexionSQL = config.GetConnectionString("ConexionSQL");
        }

        [HttpGet]
        [Route("PrimerosTresLugares")]
        public List<PremiosGlobales> GlobalesPrimerosLugares(int idLiga)
        {
            List<PremiosGlobales> CantidadPremiosGlobales = new List<PremiosGlobales>();
            using (var conexion = new SqlConnection(ConexionSQL))
            {
                var cmd = new SqlCommand("SP_PRIMEROSLUGARES_PREMIOS", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    conexion.Open();

                    using (SqlDataReader datosObtenidos = cmd.ExecuteReader())
                    {
                        while (datosObtenidos.Read())
                        {
                            CantidadPremiosGlobales.Add(new PremiosGlobales()
                            {
                                ID_LIGA = Convert.ToInt32(datosObtenidos["ID_LIGA"]),
                                NOMBRE_LIGA = datosObtenidos["NOMBRE_LIGA"].ToString(),
                                ID_USUARIO = Convert.ToInt32(datosObtenidos["ID_USUARIO"]),
                                NOMBRES = datosObtenidos["NOMBRES"].ToString(),
                                APELLIDOS = datosObtenidos["APELLIDOS"].ToString(),
                                ACUMULACION_PUNTOS = Convert.ToInt32(datosObtenidos["ACUMULACION_PUNTOS"]),
                                POSICIONFILAS = Convert.ToInt32(datosObtenidos["POSICIONFILAS"]),
                                MONTO_ASIGNADO = Convert.ToDecimal(datosObtenidos["MONTO_ASIGNADO"])
                            });
                        }
                    }
                    return CantidadPremiosGlobales;

                }
                catch (Exception ex)
                {
                    return CantidadPremiosGlobales;
                }

            }
        }




        [HttpGet]
        [Route("MejorPromedio")]
        public List<PremiosGlobales> GlobalesMejorPromedio(int idLiga)
        {
            List<PremiosGlobales> CantidadPremiosGlobales = new List<PremiosGlobales>();
            using (var conexion = new SqlConnection(ConexionSQL))
            {
                var cmd = new SqlCommand("SP_LIGAGANADORA_PREMIOS", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    conexion.Open();

                    using (SqlDataReader datosObtenidos = cmd.ExecuteReader())
                    {
                        while (datosObtenidos.Read())
                        {
                            CantidadPremiosGlobales.Add(new PremiosGlobales()
                            {
                                ID_LIGA = Convert.ToInt32(datosObtenidos["ID_LIGA"]),
                                NOMBRE_LIGA = datosObtenidos["NOMBRE_LIGA"].ToString(),
                                ID_USUARIO = Convert.ToInt32(datosObtenidos["ID_USUARIO"]),
                                NOMBRES = datosObtenidos["NOMBRES"].ToString(),
                                APELLIDOS = datosObtenidos["APELLIDOS"].ToString(),
                                MONTO_ASIGNADO = Convert.ToDecimal(datosObtenidos["MONTO_ASIGNADO"])
                            });
                        }
                    }
                    return CantidadPremiosGlobales;

                }
                catch (Exception ex)
                {
                    return CantidadPremiosGlobales;
                }

            }
        }



    }
}
