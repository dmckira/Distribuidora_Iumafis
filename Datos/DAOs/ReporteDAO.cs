using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Datos.DAOs
{
    public class ReporteDAO
    {
        public DataTable VentasPorProducto(DateTime? desde, DateTime? hasta)
        {
            var sql = @"SELECT pr.nombre AS Producto, pr.categoria AS Categoria, pr.marca AS Marca,
                        SUM(dp.cantidad) AS Unidades, SUM(dp.subtotal) AS Total
                        FROM detalle_pedido dp
                        INNER JOIN productos pr ON dp.producto_id = pr.id
                        INNER JOIN pedidos pe ON dp.pedido_id = pe.id
                        WHERE pe.estado <> 'cancelado'";
            if (desde.HasValue) sql += " AND DATE(pe.fecha_pedido) >= @desde";
            if (hasta.HasValue) sql += " AND DATE(pe.fecha_pedido) <= @hasta";
            sql += " GROUP BY pr.id, pr.nombre, pr.categoria, pr.marca ORDER BY Total DESC";
            return EjecutarDataTable(sql, desde, hasta);
        }

        public DataTable VentasPorCliente(DateTime? desde, DateTime? hasta)
        {
            var sql = @"SELECT c.nombre AS Cliente, c.tipo_cliente AS Tipo,
                        COUNT(DISTINCT pe.id) AS Pedidos, SUM(pe.total) AS Total
                        FROM pedidos pe
                        INNER JOIN clientes c ON pe.cliente_id = c.id
                        WHERE pe.estado <> 'cancelado'";
            if (desde.HasValue) sql += " AND DATE(pe.fecha_pedido) >= @desde";
            if (hasta.HasValue) sql += " AND DATE(pe.fecha_pedido) <= @hasta";
            sql += " GROUP BY c.id, c.nombre, c.tipo_cliente ORDER BY Total DESC";
            return EjecutarDataTable(sql, desde, hasta);
        }

        public DataTable VentasPorFecha(DateTime? desde, DateTime? hasta)
        {
            var sql = @"SELECT DATE(pe.fecha_pedido) AS Fecha,
                        COUNT(*) AS Pedidos, SUM(pe.total) AS Total
                        FROM pedidos pe
                        WHERE pe.estado <> 'cancelado'";
            if (desde.HasValue) sql += " AND DATE(pe.fecha_pedido) >= @desde";
            if (hasta.HasValue) sql += " AND DATE(pe.fecha_pedido) <= @hasta";
            sql += " GROUP BY DATE(pe.fecha_pedido) ORDER BY Fecha DESC";
            return EjecutarDataTable(sql, desde, hasta);
        }

        public DataTable VentasPorCategoria(DateTime? desde, DateTime? hasta)
        {
            var sql = @"SELECT pr.categoria AS Categoria,
                        SUM(dp.cantidad) AS Unidades, SUM(dp.subtotal) AS Total
                        FROM detalle_pedido dp
                        INNER JOIN productos pr ON dp.producto_id = pr.id
                        INNER JOIN pedidos pe ON dp.pedido_id = pe.id
                        WHERE pe.estado <> 'cancelado'";
            if (desde.HasValue) sql += " AND DATE(pe.fecha_pedido) >= @desde";
            if (hasta.HasValue) sql += " AND DATE(pe.fecha_pedido) <= @hasta";
            sql += " GROUP BY pr.categoria ORDER BY Total DESC";
            return EjecutarDataTable(sql, desde, hasta);
        }

        public DataTable PedidosPorEstado()
        {
            var sql = @"SELECT estado AS Estado, COUNT(*) AS Cantidad, SUM(total) AS Total
                        FROM pedidos GROUP BY estado ORDER BY Cantidad DESC";
            return EjecutarDataTable(sql, null, null);
        }

        public Dictionary<string, object> ObtenerResumenDashboard()
        {
            var resumen = new Dictionary<string, object>();
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();

                var cmd = new MySqlCommand("SELECT COUNT(*) FROM productos WHERE disponibilidad=1", conn);
                resumen["TotalProductos"] = Convert.ToInt32(cmd.ExecuteScalar());

                cmd = new MySqlCommand("SELECT COUNT(*) FROM clientes", conn);
                resumen["TotalClientes"] = Convert.ToInt32(cmd.ExecuteScalar());

                cmd = new MySqlCommand("SELECT COUNT(*) FROM pedidos WHERE estado NOT IN ('cancelado','entregado')", conn);
                resumen["PedidosPendientes"] = Convert.ToInt32(cmd.ExecuteScalar());

                cmd = new MySqlCommand("SELECT COALESCE(SUM(monto),0) FROM pagos WHERE MONTH(fecha_pago)=MONTH(NOW()) AND YEAR(fecha_pago)=YEAR(NOW())", conn);
                resumen["IngresosMes"] = Convert.ToDecimal(cmd.ExecuteScalar());

                cmd = new MySqlCommand(@"SELECT pr.nombre, SUM(dp.cantidad) AS total 
                    FROM detalle_pedido dp INNER JOIN productos pr ON dp.producto_id=pr.id
                    INNER JOIN pedidos pe ON dp.pedido_id=pe.id
                    WHERE pe.estado <> 'cancelado'
                    GROUP BY pr.id, pr.nombre ORDER BY total DESC LIMIT 5", conn);
                var topProductos = new List<string>();
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read()) topProductos.Add(dr.GetString(0) + " (" + dr.GetInt32(1) + ")");
                }
                resumen["TopProductos"] = topProductos;

                cmd = new MySqlCommand(@"SELECT c.nombre, COUNT(pe.id) AS total 
                    FROM pedidos pe INNER JOIN clientes c ON pe.cliente_id=c.id
                    WHERE pe.estado <> 'cancelado'
                    GROUP BY c.id, c.nombre ORDER BY total DESC LIMIT 5", conn);
                var topClientes = new List<string>();
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read()) topClientes.Add(dr.GetString(0) + " (" + dr.GetInt32(1) + " pedidos)");
                }
                resumen["TopClientes"] = topClientes;
            }
            return resumen;
        }

        private DataTable EjecutarDataTable(string sql, DateTime? desde, DateTime? hasta)
        {
            var dt = new DataTable();
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand(sql, conn);
                if (desde.HasValue) cmd.Parameters.AddWithValue("@desde", desde.Value.Date);
                if (hasta.HasValue) cmd.Parameters.AddWithValue("@hasta", hasta.Value.Date);
                using (var adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }
    }
}
