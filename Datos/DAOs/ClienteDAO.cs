using Datos.Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Datos.DAOs
{
    public class ClienteDAO
    {
        public List<Cliente> ObtenerTodos()
        {
            var lista = new List<Cliente>();
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM clientes ORDER BY nombre", conn);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read()) lista.Add(MapearCliente(dr));
                }
            }
            return lista;
        }

        public List<Cliente> Buscar(string nombre, string tipoCliente, string telefono, string email)
        {
            var lista = new List<Cliente>();
            var sql = "SELECT * FROM clientes WHERE 1=1";
            if (!string.IsNullOrEmpty(nombre)) sql += " AND nombre LIKE @nombre";
            if (!string.IsNullOrEmpty(tipoCliente)) sql += " AND tipo_cliente = @tipoCliente";
            if (!string.IsNullOrEmpty(telefono)) sql += " AND telefono LIKE @telefono";
            if (!string.IsNullOrEmpty(email)) sql += " AND email LIKE @email";
            sql += " ORDER BY nombre";

            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand(sql, conn);
                if (!string.IsNullOrEmpty(nombre)) cmd.Parameters.AddWithValue("@nombre", "%" + nombre + "%");
                if (!string.IsNullOrEmpty(tipoCliente)) cmd.Parameters.AddWithValue("@tipoCliente", tipoCliente);
                if (!string.IsNullOrEmpty(telefono)) cmd.Parameters.AddWithValue("@telefono", "%" + telefono + "%");
                if (!string.IsNullOrEmpty(email)) cmd.Parameters.AddWithValue("@email", "%" + email + "%");
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read()) lista.Add(MapearCliente(dr));
                }
            }
            return lista;
        }

        public Cliente ObtenerPorId(int id)
        {
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM clientes WHERE id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read()) return MapearCliente(dr);
                }
            }
            return null;
        }

        public void Insertar(Cliente c)
        {
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand(@"INSERT INTO clientes (nombre,tipo_cliente,telefono,email,direccion)
                    VALUES(@nombre,@tipo,@telefono,@email,@direccion)", conn);
                AgregarParametros(cmd, c);
                cmd.ExecuteNonQuery();
            }
        }

        public void Actualizar(Cliente c)
        {
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand(@"UPDATE clientes SET nombre=@nombre,tipo_cliente=@tipo,
                    telefono=@telefono,email=@email,direccion=@direccion WHERE id=@id", conn);
                AgregarParametros(cmd, c);
                cmd.Parameters.AddWithValue("@id", c.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(int id)
        {
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand("DELETE FROM clientes WHERE id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        private void AgregarParametros(MySqlCommand cmd, Cliente c)
        {
            cmd.Parameters.AddWithValue("@nombre", c.Nombre);
            cmd.Parameters.AddWithValue("@tipo", c.TipoCliente ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@telefono", c.Telefono ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@email", c.Email ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@direccion", c.Direccion ?? (object)DBNull.Value);
        }

        private Cliente MapearCliente(MySqlDataReader dr)
        {
            return new Cliente
            {
                Id = dr.GetInt32("id"),
                Nombre = dr.GetString("nombre"),
                TipoCliente = dr.IsDBNull(dr.GetOrdinal("tipo_cliente")) ? "" : dr.GetString("tipo_cliente"),
                Telefono = dr.IsDBNull(dr.GetOrdinal("telefono")) ? "" : dr.GetString("telefono"),
                Email = dr.IsDBNull(dr.GetOrdinal("email")) ? "" : dr.GetString("email"),
                Direccion = dr.IsDBNull(dr.GetOrdinal("direccion")) ? "" : dr.GetString("direccion"),
                FechaRegistro = dr.IsDBNull(dr.GetOrdinal("fecha_registro")) ? DateTime.Now : dr.GetDateTime("fecha_registro")
            };
        }
    }
}
