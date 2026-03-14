using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Distribuidora_Iumafis.App_Code
{
    public class Conexion
    {
        private readonly string cadenaConexion;

        public Conexion()
        {
            cadenaConexion = ConfigurationManager.ConnectionStrings["MySqlConexion"].ConnectionString;
        }

        public MySqlConnection ObtenerConexion()
        {
            return new MySqlConnection(cadenaConexion);
        }
    }
}