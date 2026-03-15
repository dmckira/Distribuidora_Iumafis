using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Distribuidora_Iumafis
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
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