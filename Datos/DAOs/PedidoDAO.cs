using Datos.Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Datos.DAOs
{
    public class PedidoDAO
    {
        public List<Pedido> ObtenerTodos()
        {
            var lista = new List<Pedido>();
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand(@"SELECT p.*, c.nombre AS cliente_nombre 
                    FROM pedidos p 
                    INNER JOIN clientes c ON p.cliente_id = c.id 
                    ORDER BY p.fecha_pedido DESC", conn);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read()) lista.Add(MapearPedido(dr));
                }
            }
            return lista;
        }

        public List<Pedido> Buscar(int? clienteId, string estado, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            var lista = new List<Pedido>();
            var sql = @"SELECT p.*, c.nombre AS cliente_nombre FROM pedidos p 
                        INNER JOIN clientes c ON p.cliente_id = c.id WHERE 1=1";
            if (clienteId.HasValue) sql += " AND p.cliente_id = @clienteId";
            if (!string.IsNullOrEmpty(estado)) sql += " AND p.estado = @estado";
            if (fechaDesde.HasValue) sql += " AND DATE(p.fecha_pedido) >= @fechaDesde";
            if (fechaHasta.HasValue) sql += " AND DATE(p.fecha_pedido) <= @fechaHasta";
            sql += " ORDER BY p.fecha_pedido DESC";

            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand(sql, conn);
                if (clienteId.HasValue) cmd.Parameters.AddWithValue("@clienteId", clienteId.Value);
                if (!string.IsNullOrEmpty(estado)) cmd.Parameters.AddWithValue("@estado", estado);
                if (fechaDesde.HasValue) cmd.Parameters.AddWithValue("@fechaDesde", fechaDesde.Value.Date);
                if (fechaHasta.HasValue) cmd.Parameters.AddWithValue("@fechaHasta", fechaHasta.Value.Date);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read()) lista.Add(MapearPedido(dr));
                }
            }
            return lista;
        }

        public Pedido ObtenerPorId(int id)
        {
            Pedido pedido = null;
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand(@"SELECT p.*, c.nombre AS cliente_nombre 
                    FROM pedidos p INNER JOIN clientes c ON p.cliente_id = c.id 
                    WHERE p.id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read()) pedido = MapearPedido(dr);
                }
                if (pedido != null)
                    pedido.Detalles = ObtenerDetalles(id, conn);
            }
            return pedido;
        }

        public int Insertar(Pedido p)
        {
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        var cmd = new MySqlCommand(@"INSERT INTO pedidos (cliente_id,estado,total) 
                            VALUES(@clienteId,@estado,@total); SELECT LAST_INSERT_ID();", conn, trans);
                        cmd.Parameters.AddWithValue("@clienteId", p.ClienteId);
                        cmd.Parameters.AddWithValue("@estado", p.Estado ?? "pendiente");
                        cmd.Parameters.AddWithValue("@total", p.Total);
                        int nuevoId = Convert.ToInt32(cmd.ExecuteScalar());

                        foreach (var d in p.Detalles)
                        {
                            var cmdD = new MySqlCommand(@"INSERT INTO detalle_pedido (pedido_id,producto_id,cantidad,precio_unitario,subtotal)
                                VALUES(@pedidoId,@productoId,@cantidad,@precio,@subtotal)", conn, trans);
                            cmdD.Parameters.AddWithValue("@pedidoId", nuevoId);
                            cmdD.Parameters.AddWithValue("@productoId", d.ProductoId);
                            cmdD.Parameters.AddWithValue("@cantidad", d.Cantidad);
                            cmdD.Parameters.AddWithValue("@precio", d.PrecioUnitario);
                            cmdD.Parameters.AddWithValue("@subtotal", d.Subtotal);
                            cmdD.ExecuteNonQuery();

                            var cmdStock = new MySqlCommand("UPDATE productos SET stock = stock - @cant WHERE id = @pid", conn, trans);
                            cmdStock.Parameters.AddWithValue("@cant", d.Cantidad);
                            cmdStock.Parameters.AddWithValue("@pid", d.ProductoId);
                            cmdStock.ExecuteNonQuery();
                        }

                        trans.Commit();
                        return nuevoId;
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public void ActualizarEstado(int id, string estado)
        {
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand("UPDATE pedidos SET estado=@estado WHERE id=@id", conn);
                cmd.Parameters.AddWithValue("@estado", estado);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarTotal(int id, decimal total)
        {
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand("UPDATE pedidos SET total=@total WHERE id=@id", conn);
                cmd.Parameters.AddWithValue("@total", total);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(int id)
        {
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        var cmdD = new MySqlCommand("DELETE FROM detalle_pedido WHERE pedido_id=@id", conn, trans);
                        cmdD.Parameters.AddWithValue("@id", id);
                        cmdD.ExecuteNonQuery();

                        var cmdP = new MySqlCommand("DELETE FROM pagos WHERE pedido_id=@id", conn, trans);
                        cmdP.Parameters.AddWithValue("@id", id);
                        cmdP.ExecuteNonQuery();

                        var cmd = new MySqlCommand("DELETE FROM pedidos WHERE id=@id", conn, trans);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<Pedido> ObtenerPorCliente(int clienteId)
        {
            var lista = new List<Pedido>();
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand(@"SELECT p.*, c.nombre AS cliente_nombre FROM pedidos p 
                    INNER JOIN clientes c ON p.cliente_id = c.id 
                    WHERE p.cliente_id=@clienteId ORDER BY p.fecha_pedido DESC", conn);
                cmd.Parameters.AddWithValue("@clienteId", clienteId);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read()) lista.Add(MapearPedido(dr));
                }
            }
            return lista;
        }

        private List<DetallePedido> ObtenerDetalles(int pedidoId, MySqlConnection conn)
        {
            var lista = new List<DetallePedido>();
            var cmd = new MySqlCommand(@"SELECT dp.*, pr.nombre AS producto_nombre 
                FROM detalle_pedido dp 
                INNER JOIN productos pr ON dp.producto_id = pr.id 
                WHERE dp.pedido_id=@pedidoId", conn);
            cmd.Parameters.AddWithValue("@pedidoId", pedidoId);
            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    lista.Add(new DetallePedido
                    {
                        Id = dr.GetInt32("id"),
                        PedidoId = dr.GetInt32("pedido_id"),
                        ProductoId = dr.GetInt32("producto_id"),
                        ProductoNombre = dr.GetString("producto_nombre"),
                        Cantidad = dr.GetInt32("cantidad"),
                        PrecioUnitario = dr.GetDecimal("precio_unitario"),
                        Subtotal = dr.GetDecimal("subtotal")
                    });
                }
            }
            return lista;
        }

        private Pedido MapearPedido(MySqlDataReader dr)
        {
            return new Pedido
            {
                Id = dr.GetInt32("id"),
                ClienteId = dr.GetInt32("cliente_id"),
                ClienteNombre = dr.GetString("cliente_nombre"),
                FechaPedido = dr.IsDBNull(dr.GetOrdinal("fecha_pedido")) ? DateTime.Now : dr.GetDateTime("fecha_pedido"),
                Estado = dr.GetString("estado"),
                Total = dr.IsDBNull(dr.GetOrdinal("total")) ? 0 : dr.GetDecimal("total")
            };
        }
    }
}
