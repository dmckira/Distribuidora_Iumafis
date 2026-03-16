using Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Distribuidora_Iumafis
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarDashboard();
        }

        private void CargarDashboard()
        {
            try
            {
                var svc = new ReporteNegocio();
                var resumen = svc.ObtenerResumenDashboard();

                lblTotalProductos.Text = resumen["TotalProductos"].ToString();
                lblTotalClientes.Text = resumen["TotalClientes"].ToString();
                lblPedidosPendientes.Text = resumen["PedidosPendientes"].ToString();
                lblIngresosMes.Text = string.Format("{0:N2}", resumen["IngresosMes"]);

                var topProductos = resumen["TopProductos"] as List<string>;
                if (topProductos != null && topProductos.Count > 0)
                {
                    rptTopProductos.DataSource = topProductos;
                    rptTopProductos.DataBind();
                }
                else
                {
                    lblNoTopProductos.Text = "Sin datos disponibles.";
                }

                var topClientes = resumen["TopClientes"] as List<string>;
                if (topClientes != null && topClientes.Count > 0)
                {
                    rptTopClientes.DataSource = topClientes;
                    rptTopClientes.DataBind();
                }
                else
                {
                    lblNoTopClientes.Text = "Sin datos disponibles.";
                }
            }
            catch (Exception ex)
            {
                lblTotalProductos.Text = "0";
                lblTotalClientes.Text = "0";
                lblPedidosPendientes.Text = "0";
                lblIngresosMes.Text = "0.00";
            }
        }
    }
}