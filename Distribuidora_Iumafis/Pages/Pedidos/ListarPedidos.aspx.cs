using Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Distribuidora_Iumafis.Pages.Pedidos
{
    public partial class ListarPedidos : System.Web.UI.Page
    {
        private readonly PedidoNegocio svc = new PedidoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPedidos();
                if (Request.QueryString["msg"] == "creado")
                    MostrarAlerta("Pedido creado exitosamente.", "alert-success");
                else if (Request.QueryString["msg"] == "editado")
                    MostrarAlerta("Estado del pedido actualizado.", "alert-success");
                else if (Request.QueryString["msg"] == "eliminado")
                    MostrarAlerta("Pedido eliminado exitosamente.", "alert-success");
            }
        }

        private void CargarPedidos()
        {
            DateTime? desde = null, hasta = null;
            if (DateTime.TryParse(txtDesde.Text, out DateTime d)) desde = d;
            if (DateTime.TryParse(txtHasta.Text, out DateTime h)) hasta = h;
            var lista = svc.Buscar(null, ddlEstado.SelectedValue, desde, hasta);
            gvPedidos.DataSource = lista;
            gvPedidos.DataBind();
            lblTotal.Text = lista.Count.ToString();
            lblVacio.Visible = lista.Count == 0;
        }

        protected void btnBuscar_Click(object sender, EventArgs e) => CargarPedidos();

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            ddlEstado.SelectedIndex = 0;
            txtDesde.Text = txtHasta.Text = "";
            CargarPedidos();
        }

        protected void gvPedidos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "Detalle")
                Response.Redirect("DetallePedido.aspx?id=" + id);
            else if (e.CommandName == "Editar")
                Response.Redirect("EditarPedido.aspx?id=" + id);
            else if (e.CommandName == "Pago")
                Response.Redirect("../Pagos/CrearPago.aspx?pedidoId=" + id);
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    svc.Eliminar(id);
                    MostrarAlerta("Pedido eliminado exitosamente.", "alert-success");
                    CargarPedidos();
                }
                catch (Exception ex)
                {
                    MostrarAlerta("Error al eliminar: " + ex.Message, "alert-danger");
                }
            }
        }

        protected string GetBadgeEstado(string estado)
        {
            switch (estado)
            {
                case "entregado": return "badge badge-success";
                case "cancelado": return "badge badge-danger";
                case "enviado": return "badge badge-info";
                case "en preparacion": return "badge badge-warning";
                default: return "badge badge-secondary";
            }
        }

        private void MostrarAlerta(string msg, string tipo)
        {
            pnlAlerta.Visible = true;
            pnlAlerta.CssClass = "alert " + tipo;
            lblAlerta.Text = msg;
        }
    }
}