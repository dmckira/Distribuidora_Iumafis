using Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Distribuidora_Iumafis.Pages.Pedidos
{
    public partial class EditarPedido : System.Web.UI.Page
    {
        private readonly PedidoNegocio svc = new PedidoNegocio();
        public int PedidoId => int.TryParse(Request.QueryString["id"], out int id) ? id : 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (PedidoId == 0) { Response.Redirect("ListarPedidos.aspx"); return; }
            if (!IsPostBack) CargarPedido();
        }

        private void CargarPedido()
        {
            var p = svc.ObtenerPorId(PedidoId);
            if (p == null) { Response.Redirect("ListarPedidos.aspx"); return; }
            lblId.Text = p.Id.ToString();
            lblCliente.Text = p.ClienteNombre;
            lblTotal.Text = string.Format("{0:C2}", p.Total);
            if (ddlEstado.Items.FindByValue(p.Estado) != null)
                ddlEstado.SelectedValue = p.Estado;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                svc.ActualizarEstado(PedidoId, ddlEstado.SelectedValue);
                Response.Redirect("ListarPedidos.aspx?msg=editado");
            }
            catch (Exception ex)
            {
                pnlAlerta.Visible = true;
                pnlAlerta.CssClass = "alert alert-danger";
                lblAlerta.Text = "Error: " + ex.Message;
            }
        }
    }
}