using MySql.Data.MySqlClient;
using System.Configuration;

namespace Datos.DAOs
{
    public class ConexionMySQL
    {
        private static readonly string cadena =
            ConfigurationManager.ConnectionStrings["MySqlConexion"].ConnectionString;

        public static MySqlConnection ObtenerConexion()
        {
            return new MySqlConnection(cadena);
        }
    }
}
