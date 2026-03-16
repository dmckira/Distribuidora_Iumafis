using Datos.Entidades;
using Negocio.Servicios;
using System;
using System.Web.UI.WebControls;

namespace Distribuidora_Iumafis.Pages.Pagos
{
    public partial class EditarPago : System.Web.UI.Page
    {
        private readonly PagoNegocio svc = new PagoNegocio();
        private readonly PedidoNegocio pedidoSvc = new PedidoNegocio();
        private int PagoId => int.TryParse(Request.QueryString["id"], out int id) ? id : 0;
        private bool ModoVer => Request.QueryString["modo"] == "ver";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (PagoId == 0) { Response.Redirect("ListarPagos.aspx"); return; }
            if (!IsPostBack) CargarPago();
        }

        private void CargarPago()
        {
            var p = svc.ObtenerPorId(PagoId);
            if (p == null) { Response.Redirect("ListarPagos.aspx"); return; }

            CargarPedidos();
            if (ddlPedido.Items.FindByValue(p.PedidoId.ToString()) != null)
                ddlPedido.SelectedValue = p.PedidoId.ToString();
            txtCliente.Text = p.ClienteNombre;
            if (ddlTipoPago.Items.FindByValue(p.TipoPago) != null)
                ddlTipoPago.SelectedValue = p.TipoPago;
            txtMonto.Text = p.Monto.ToString("N2");
            txtCuotas.Text = p.Cuotas.ToString();
            txtFecha.Text = p.FechaPago.ToString("dd/MM/yyyy HH:mm");

            if (ModoVer)
            {
                lblTitulo.Text = "<i class='fa fa-receipt'></i> Comprobante de Pago";
                lblSubtitulo.Text = "Ver";
                ddlPedido.Enabled = false;
                ddlTipoPago.Enabled = false;
                txtMonto.ReadOnly = true;
                txtCuotas.ReadOnly = true;
                pnlAcciones.Visible = false;
                pnlBtnImprimir.Visible = true;
            }
        }

        private void CargarPedidos()
        {
            ddlPedido.Items.Clear();
            ddlPedido.Items.Add(new ListItem("-- Pedido --", ""));
            foreach (var ped in pedidoSvc.ObtenerTodos())
                ddlPedido.Items.Add(new ListItem(
                    "#" + ped.Id + " - " + ped.ClienteNombre,
                    ped.Id.ToString()));
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            try
            {
                var pago = new Pago
                {
                    Id = PagoId,
                    PedidoId = int.Parse(ddlPedido.SelectedValue),
                    TipoPago = ddlTipoPago.SelectedValue,
                    Monto = decimal.Parse(txtMonto.Text),
                    Cuotas = int.TryParse(txtCuotas.Text, out int c) && c >= 1 ? c : 1
                };
                svc.Guardar(pago);
                Response.Redirect("ListarPagos.aspx?msg=editado");
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