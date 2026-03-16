using Negocio.Servicios;
using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Distribuidora_Iumafis.Pages.Usuarios
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioId"] != null)
                Response.Redirect("~/default.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var svc = new UsuarioNegocio();
                var usuario = svc.Autenticar(txtUsuario.Text.Trim(), txtPassword.Text);
                if (usuario != null)
                {
                    Session["UsuarioId"] = usuario.Id;
                    Session["UsuarioNombre"] = usuario.Nombre;
                    Session["UsuarioLogin"] = usuario.NombreUsuario;
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    pnlError.Visible = true;
                    lblError.Text = "Usuario o contraseña incorrectos.";
                }
            }
            catch (Exception ex)
            {
                pnlError.Visible = true;
                lblError.Text = "Error: " + ex.Message;
            }
        }
    }
}