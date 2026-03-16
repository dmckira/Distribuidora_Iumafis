using Negocio.Servicios;
using System;
using System.Data;
using System.Linq;

namespace Distribuidora_Iumafis.Pages.Reportes
{
    public partial class Reportes : System.Web.UI.Page
    {
        private readonly ReporteNegocio svc = new ReporteNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                GenerarReportes();
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            GenerarReportes();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtDesde.Text = txtHasta.Text = "";
            GenerarReportes();
        }

        private void GenerarReportes()
        {
            DateTime? desde = null, hasta = null;
            if (DateTime.TryParse(txtDesde.Text, out DateTime d)) desde = d;
            if (DateTime.TryParse(txtHasta.Text, out DateTime h)) hasta = h;

            // Ventas por producto
            var dtProductos = svc.VentasPorProducto(desde, hasta);
            gvProductos.DataSource = dtProductos;
            gvProductos.DataBind();
            lblProductosVacio.Visible = dtProductos.Rows.Count == 0;

            // Ventas por cliente
            var dtClientes = svc.VentasPorCliente(desde, hasta);
            gvClientes.DataSource = dtClientes;
            gvClientes.DataBind();
            lblClientesVacio.Visible = dtClientes.Rows.Count == 0;

            // Ventas por fecha
            var dtFechas = svc.VentasPorFecha(desde, hasta);
            gvFechas.DataSource = dtFechas;
            gvFechas.DataBind();
            lblFechasVacio.Visible = dtFechas.Rows.Count == 0;

            // Ventas por categoria
            var dtCategorias = svc.VentasPorCategoria(desde, hasta);
            gvCategorias.DataSource = dtCategorias;
            gvCategorias.DataBind();
            lblCategoriasVacio.Visible = dtCategorias.Rows.Count == 0;

            // Estados de pedidos
            var dtEstados = svc.PedidosPorEstado();
            gvEstados.DataSource = dtEstados;
            gvEstados.DataBind();

            // Calcular resumen
            decimal totalVentas = 0;
            int totalPedidos = 0;
            int totalUnidades = 0;
            int totalCancelados = 0;

            foreach (DataRow row in dtFechas.Rows)
            {
                totalVentas += Convert.ToDecimal(row["Total"]);
                totalPedidos += Convert.ToInt32(row["Pedidos"]);
            }
            foreach (DataRow row in dtProductos.Rows)
                totalUnidades += Convert.ToInt32(row["Unidades"]);
            foreach (DataRow row in dtEstados.Rows)
                if (row["Estado"].ToString() == "cancelado")
                    totalCancelados = Convert.ToInt32(row["Cantidad"]);

            lblTotalVentas.Text = string.Format("{0:C2}", totalVentas);
            lblTotalPedidos.Text = totalPedidos.ToString();
            lblTotalProductosVendidos.Text = totalUnidades.ToString();
            lblTotalCancelados.Text = totalCancelados.ToString();
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
    }
}
