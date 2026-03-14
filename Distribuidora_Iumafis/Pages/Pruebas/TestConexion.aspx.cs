using System;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Distribuidora_Iumafis.Pages
{
    public partial class TestConexion : System.Web.UI.Page
    {
        protected void btnProbar_Click(object sender, EventArgs e)
        {
            string cadena = ConfigurationManager.ConnectionStrings["MySqlConexion"].ConnectionString;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(cadena))
                {
                    conn.Open();
                    lblResultado.Text = "Conexión exitosa a MySQL";
                    lblResultado.ForeColor = System.Drawing.Color.Green;
                }
            }
            catch (Exception ex)
            {
                lblResultado.Text = "Error al conectar: " + ex.Message;
                lblResultado.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}

