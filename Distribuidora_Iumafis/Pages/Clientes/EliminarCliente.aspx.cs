using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Distribuidora_Iumafis.Pages.Clientes
{
    public partial class EliminarCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (int.TryParse(Request.QueryString["id"], out int id) && id > 0)
            {
                try { new Negocio.Servicios.ClienteNegocio().Eliminar(id); Response.Redirect("ListarClientes.aspx?msg=eliminado"); }
                catch { Response.Redirect("ListarClientes.aspx?msg=error"); }
            }
            else Response.Redirect("ListarClientes.aspx");
        }
    }
}