using System;
using System.Web;
using System.Web.UI;

namespace Distribuidora_Iumafis
{
    public partial class Site : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioId"] == null && !Request.Url.AbsolutePath.ToLower().Contains("login"))
            {
                Response.Redirect("~/Pages/Usuarios/Login.aspx");
            }
        }
    }
}
