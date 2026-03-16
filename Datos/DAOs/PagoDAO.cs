using Datos.Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Datos.DAOs
{
    public class PagoDAO
    {
        public List<Pago> ObtenerTodos()
        {
            var lista = new List<Pago>();
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand(@"SELECT pa.*, c.nombre AS cliente_nombre 
                    FROM pagos pa 
                    INNER JOIN pedidos pe ON pa.pedido_id = pe.id 
                    INNER JOIN clientes c ON pe.cliente_id = c.id 
                    ORDER BY pa.fecha_pago DESC", conn);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read()) lista.Add(MapearPago(dr));
                }
            }
            return lista;
        }

        public List<Pago> Buscar(int? clienteId, string tipoPago, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            var lista = new List<Pago>();
            var sql = @"SELECT pa.*, c.nombre AS cliente_nombre FROM pagos pa 
                        INNER JOIN pedidos pe ON pa.pedido_id = pe.id 
                        INNER JOIN clientes c ON pe.cliente_id = c.id WHERE 1=1";
            if (clienteId.HasValue) sql += " AND pe.cliente_id = @clienteId";
            if (!string.IsNullOrEmpty(tipoPago)) sql += " AND pa.tipo_pago = @tipoPago";
            if (fechaDesde.HasValue) sql += " AND DATE(pa.fecha_pago) >= @fechaDesde";
            if (fechaHasta.HasValue) sql += " AND DATE(pa.fecha_pago) <= @fechaHasta";
            sql += " ORDER BY pa.fecha_pago DESC";

            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand(sql, conn);
                if (clienteId.HasValue) cmd.Parameters.AddWithValue("@clienteId", clienteId.Value);
                if (!string.IsNullOrEmpty(tipoPago)) cmd.Parameters.AddWithValue("@tipoPago", tipoPago);
                if (fechaDesde.HasValue) cmd.Parameters.AddWithValue("@fechaDesde", fechaDesde.Value.Date);
                if (fechaHasta.HasValue) cmd.Parameters.AddWithValue("@fechaHasta", fechaHasta.Value.Date);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read()) lista.Add(MapearPago(dr));
                }
            }
            return lista;
        }

        public Pago ObtenerPorId(int id)
        {
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand(@"SELECT pa.*, c.nombre AS cliente_nombre FROM pagos pa 
                    INNER JOIN pedidos pe ON pa.pedido_id = pe.id 
                    INNER JOIN clientes c ON pe.cliente_id = c.id 
                    WHERE pa.id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read()) return MapearPago(dr);
                }
            }
            return null;
        }

        public void Insertar(Pago p)
        {
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand(@"INSERT INTO pagos (pedido_id,tipo_pago,monto,cuotas)
                    VALUES(@pedidoId,@tipoPago,@monto,@cuotas)", conn);
                AgregarParametros(cmd, p);
                cmd.ExecuteNonQuery();
            }
        }

        public void Actualizar(Pago p)
        {
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand(@"UPDATE pagos SET pedido_id=@pedidoId,tipo_pago=@tipoPago,
                    monto=@monto,cuotas=@cuotas WHERE id=@id", conn);
                AgregarParametros(cmd, p);
                cmd.Parameters.AddWithValue("@id", p.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(int id)
        {
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand("DELETE FROM pagos WHERE id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Pago> ObtenerPorPedido(int pedidoId)
        {
            var lista = new List<Pago>();
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand(@"SELECT pa.*, c.nombre AS cliente_nombre FROM pagos pa 
                    INNER JOIN pedidos pe ON pa.pedido_id = pe.id 
                    INNER JOIN clientes c ON pe.cliente_id = c.id 
                    WHERE pa.pedido_id=@pedidoId", conn);
                cmd.Parameters.AddWithValue("@pedidoId", pedidoId);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read()) lista.Add(MapearPago(dr));
                }
            }
            return lista;
        }

        private void AgregarParametros(MySqlCommand cmd, Pago p)
        {
            cmd.Parameters.AddWithValue("@pedidoId", p.PedidoId);
            cmd.Parameters.AddWithValue("@tipoPago", p.TipoPago ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@monto", p.Monto);
            cmd.Parameters.AddWithValue("@cuotas", p.Cuotas);
        }

        private Pago MapearPago(MySqlDataReader dr)
        {
            return new Pago
            {
                Id = dr.GetInt32("id"),
                PedidoId = dr.GetInt32("pedido_id"),
                ClienteNombre = dr.IsDBNull(dr.GetOrdinal("cliente_nombre")) ? "" : dr.GetString("cliente_nombre"),
                TipoPago = dr.IsDBNull(dr.GetOrdinal("tipo_pago")) ? "" : dr.GetString("tipo_pago"),
                Monto = dr.GetDecimal("monto"),
                FechaPago = dr.IsDBNull(dr.GetOrdinal("fecha_pago")) ? DateTime.Now : dr.GetDateTime("fecha_pago"),
                Cuotas = dr.GetInt32("cuotas")
            };
        }
    }
}
