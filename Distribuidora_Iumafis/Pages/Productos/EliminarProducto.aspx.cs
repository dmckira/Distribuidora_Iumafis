using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Distribuidora_Iumafis.Pages.Productos
{
    public partial class EliminarProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (int.TryParse(Request.QueryString["id"], out int id) && id > 0)
            {
                try
                {
                    new Negocio.Servicios.ProductoNegocio().Eliminar(id);
                    Response.Redirect("ListarProductos.aspx?msg=eliminado");
                }
                catch
                {
                    Response.Redirect("ListarProductos.aspx?msg=error");
                }
            }
            else
            {
                Response.Redirect("ListarProductos.aspx");
            }
        }
    }
}