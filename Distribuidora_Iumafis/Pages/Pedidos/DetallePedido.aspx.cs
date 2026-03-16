using Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Distribuidora_Iumafis.Pages.Pedidos
{
    public partial class DetallePedido : System.Web.UI.Page
    {
        private readonly PedidoNegocio svc = new PedidoNegocio();
        private readonly PagoNegocio pagoSvc = new PagoNegocio();
        public int PedidoId => int.TryParse(Request.QueryString["id"], out int id) ? id : 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (PedidoId == 0) { Response.Redirect("ListarPedidos.aspx"); return; }
            if (!IsPostBack)
            {
                CargarDetalle();
                if (Request.QueryString["msg"] == "creado")
                    MostrarAlerta("Pedido creado exitosamente.", "alert-success");
            }
        }

        private void CargarDetalle()
        {
            var p = svc.ObtenerPorId(PedidoId);
            if (p == null) { Response.Redirect("ListarPedidos.aspx"); return; }

            lblPedidoId.Text = p.Id.ToString();
            lblFecha.Text = p.FechaPedido.ToString("dd/MM/yyyy HH:mm");
            lblCliente.Text = p.ClienteNombre;
            lblEstado.Text = p.Estado.ToUpper();
            lblTotal.Text = string.Format("{0:C2}", p.Total);

            rptDetalles.DataSource = p.Detalles;
            rptDetalles.DataBind();

            var pagos = pagoSvc.ObtenerPorPedido(PedidoId);
            gvPagos.DataSource = pagos;
            gvPagos.DataBind();
            lblPagosVacio.Visible = pagos.Count == 0;
        }

        private void MostrarAlerta(string msg, string tipo)
        {
            pnlAlerta.Visible = true;
            pnlAlerta.CssClass = "alert " + tipo;
            lblAlerta.Text = msg;
        }
    }
}