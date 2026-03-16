using Datos.DAOs;
using System;
using System.Data;

namespace Negocio.Servicios
{
    public class ReporteNegocio
    {
        private readonly ReporteDAO dao = new ReporteDAO();

        public DataTable VentasPorProducto(DateTime? desde, DateTime? hasta)
        {
            return dao.VentasPorProducto(desde, hasta);
        }

        public DataTable VentasPorCliente(DateTime? desde, DateTime? hasta)
        {
            return dao.VentasPorCliente(desde, hasta);
        }

        public DataTable VentasPorFecha(DateTime? desde, DateTime? hasta)
        {
            return dao.VentasPorFecha(desde, hasta);
        }

        public DataTable VentasPorCategoria(DateTime? desde, DateTime? hasta)
        {
            return dao.VentasPorCategoria(desde, hasta);
        }

        public DataTable PedidosPorEstado()
        {
            return dao.PedidosPorEstado();
        }

        public System.Collections.Generic.Dictionary<string, object> ObtenerResumenDashboard()
        {
            return dao.ObtenerResumenDashboard();
        }
    }
}
