using Datos.Entidades;
using Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Distribuidora_Iumafis.Pages.Pagos
{
    public partial class CrearPago : System.Web.UI.Page
    {
        private readonly PagoNegocio svc = new PagoNegocio();
        private readonly PedidoNegocio pedidoSvc = new PedidoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPedidos();
                if (int.TryParse(Request.QueryString["pedidoId"], out int pedId) && pedId > 0)
                {
                    var p = pedidoSvc.ObtenerPorId(pedId);
                    if (p != null)
                    {
                        ddlPedido.SelectedValue = pedId.ToString();
                        pnlInfoPedido.Visible = true;
                        lblPedidoInfo.Text = p.Id.ToString();
                        lblClienteInfo.Text = p.ClienteNombre;
                        lblTotalInfo.Text = p.Total.ToString("N2");
                        txtMonto.Text = p.Total.ToString("N2");
                    }
                }
            }
        }

        private void CargarPedidos()
        {
            ddlPedido.Items.Clear();
            ddlPedido.Items.Add(new ListItem("-- Seleccione pedido --", ""));
            foreach (var p in pedidoSvc.ObtenerTodos())
                ddlPedido.Items.Add(new ListItem(
                    "#" + p.Id + " - " + p.ClienteNombre + " ($" + p.Total.ToString("N2") + ") [" + p.Estado + "]",
                    p.Id.ToString()));
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            try
            {
                var pago = new Pago
                {
                    PedidoId = int.Parse(ddlPedido.SelectedValue),
                    TipoPago = ddlTipoPago.SelectedValue,
                    Monto = decimal.Parse(txtMonto.Text),
                    Cuotas = int.TryParse(txtCuotas.Text, out int c) && c >= 1 ? c : 1
                };
                svc.Guardar(pago);
                Response.Redirect("ListarPagos.aspx?msg=creado");
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